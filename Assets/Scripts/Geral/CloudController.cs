using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private Transform playerPos;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] cloudsSprite;

    private Vector3 initPos;
    private bool canReset = false;

    private void Start()
    {
        initPos = transform.position;
        ResetCloud();
    }

    void Update()
    {
        //Move the cloud
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    private void OnBecameVisible()
    {
        canReset = true;
    }

    private void OnBecameInvisible()
    {
        if (canReset)
        {
            ResetCloud();
            canReset = false;
        }
    }

    private void ResetCloud()
    {
        transform.position = new Vector3(initPos.x + playerPos.position.x, initPos.y,initPos.z);
        Vector3 randScale = new Vector3(Random.Range(2.5f, 3.5f), Random.Range(2.5f, 3.5f), Random.Range(2.5f, 3.5f));
        transform.localScale = randScale;
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        spriteRenderer.sprite = cloudsSprite[Random.Range(0, cloudsSprite.Length)];
        spriteRenderer.flipX = randomBool();
    }

    bool randomBool()
    {
        return Random.Range(0, 2) == 1;
    }
}
