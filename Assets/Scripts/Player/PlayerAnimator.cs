using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator gfxAnim;

    public bool boolGrounded(bool _isGrounded)
    {
        gfxAnim.SetBool("isGrounded", _isGrounded);
        return _isGrounded;
    }

    public void Jump()
    {
        gfxAnim.SetTrigger("Jump");
    }

    public void Run()
    {
        gfxAnim.SetBool("Idle", false);
        gfxAnim.SetBool("Run", true);
    }

    public void Idle()
    {
        gfxAnim.SetBool("Idle", true);
        gfxAnim.SetBool("Run", false);
    }
}
