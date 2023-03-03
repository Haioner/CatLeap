using UnityEngine.EventSystems;
using UnityEngine;

public class InfinitePlayerController : MonoBehaviour
{
    #region Variables
    [Header("Run")]
    [SerializeField] private Transform facePos;
    [SerializeField] private float faceRadius = 0.3f;
    [SerializeField] private float speed = 6;
    private bool _canRun;
    private bool _isFacingRight = true;
    private bool _flipOnce = true;

    [Header("Jump")]
    [SerializeField] private Transform groundPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpxForceX = 13;
    [SerializeField] private float jumpForceY = 12;
    [SerializeField] private float groundRadius = 0.1f;
    [SerializeField] private GameObject jumpParticle;
    private bool _canJump;

    [Header("WallJump")]
    [SerializeField] private Transform backPos;
    [SerializeField] private float backRadius = 0.1f;
    [SerializeField] private float wallSlideSpeed = 1;
    [SerializeField] private float wallJumpTime = 0.05f;
    [SerializeField] private ParticleSystem wallSlideParticle;
    private bool _canWallJump = true;
    private bool _isBack;
    private bool _wallJumping;

    [Header("Cache")]
    [SerializeField] private PlayerAnimator anim;
    [SerializeField] private Rigidbody2D rb;
    private AudioManager audioMan;
    #endregion

    #region Methods
    private void Awake()
    {
        audioMan = AudioManager.instance;
    }

    void Update()
    {
        MoveForward();
        Jump();
        WallJump();
    }
    #endregion

    #region Custom Methods
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundPos.position, groundRadius, groundLayer);
    }

    private void MoveForward()
    {
        if (!_canRun) return;

        if (isGrounded())
        {
            transform.position += transform.right * speed * Time.deltaTime;
            anim.Run();
        }
        IncreaseSpeedByTime();
        Flip();
    }

    private void IncreaseSpeedByTime()
    {
        if (speed < 10)
            speed += Time.deltaTime * 0.01f / 2;
    }

    private bool isFront()
    {
        return Physics2D.Raycast(facePos.position, transform.right, faceRadius, groundLayer);
    }

    private void Flip()
    {
        if (isFront() && _flipOnce)
        {
            transform.localRotation = _isFacingRight ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            _flipOnce = false;
            _isFacingRight = !_isFacingRight;
        }
        else _flipOnce = true;
    }

    public void ResetFlip()
    {
        if(!_isFacingRight)
            transform.localRotation = _isFacingRight ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public void Jump()
    {
        if (!_canJump) return;
        //Set isGround Animation
        anim.boolGrounded(isGrounded());

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded())
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //Check if touch is not over button
            {
                //Reset velocity
                rb.velocity = Vector2.zero;
                //Add X and Y force
                rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);
                rb.AddForce(transform.right * jumpxForceX, ForceMode2D.Impulse);
                //Instantiate jump particle
                Instantiate(jumpParticle, groundPos.position, Quaternion.identity);
                //Play jump anim/audio
                anim.Invoke("Jump", 0.02f);
                audioMan.Play("Jump");
            }
        }

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && isGrounded() && !EventSystem.current.IsPointerOverGameObject())
        {
            //Reset velocity
            rb.velocity = Vector2.zero;
            //Add X and Y force
            rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);
            rb.AddForce(transform.right * jumpxForceX, ForceMode2D.Impulse);
            //Instantiate jump particle
            Instantiate(jumpParticle, groundPos.position, Quaternion.identity);
            //Play jump anim/audio
            anim.Invoke("Jump", 0.02f);
            audioMan.Play("Jump");
        }
        #endif
    }

    public void WallJump()
    {
        if (!_canWallJump) return;

        //Check if is something in player back
        var emission = wallSlideParticle.emission;
        _isBack = Physics2D.Raycast(backPos.position, -transform.right , backRadius, groundLayer);

        if (_isBack)
        {
            //Enable wall particle
            emission.rateOverTime = 7;
            //Set wall slide velocity
            rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            anim.Invoke("Jump", 0.02f);

            //If touches and is not on button 
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //Jump
                    _wallJumping = true;
                    Invoke("SetWallJumpFalse", wallJumpTime);
                    Instantiate(jumpParticle, groundPos.position, Quaternion.identity);
                    audioMan.Play("Jump");
                }
            }

            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                _wallJumping = true;
                Invoke("SetWallJumpFalse", wallJumpTime);
                Instantiate(jumpParticle, groundPos.position, Quaternion.identity);
                audioMan.Play("Jump");
            }
            #endif
        }
        else
        {
            //Disable wall particle
            emission.rateOverTime = 0;
        }

        //Get x force facing value
        float xFacing = _isFacingRight ? xFacing = 1 : xFacing = -1;

        //Set velocity jump
        if (_wallJumping)
            rb.velocity = new Vector2(jumpxForceX * xFacing, jumpForceY);
    }

    void SetWallJumpFalse()
    {
        _wallJumping = false;
    }

    public void SetWallSpeed(float wallSpeed)
    {
        wallSlideSpeed = wallSpeed;
    }

    public void SetAllControls(bool state)
    {
        _canRun = state;
        _canJump = state;
        _canWallJump = state;
        rb.simulated = state;
    }

    public void IdlePlayerAnim()
    {
        anim.Idle();
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPos.position, groundRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(facePos.position, transform.right * faceRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(backPos.position, -transform.right * backRadius);
    }
    #endif
    #endregion
}
