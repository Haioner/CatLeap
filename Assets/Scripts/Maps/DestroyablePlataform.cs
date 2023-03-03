using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyablePlataform : MonoBehaviour
{
    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private DOTweenAnimation dotAnim;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isPlaying;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPlaying)
        {
            dotAnim.DORestart();
            isPlaying = true;
        }
    }

    public void DestroyObject()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        boxCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        Invoke("BackObject", 3);
    }

    public void BackObject()
    {
        isPlaying = false;
        boxCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }
}
