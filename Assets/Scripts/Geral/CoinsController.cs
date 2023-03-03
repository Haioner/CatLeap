using DG.Tweening;
using UnityEngine;
using TMPro;

public class CoinsController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Animation Fade")]
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private DOTweenAnimation dotAnim;
    [SerializeField] private Color newColor;

    private void Update()
    {
        if (!Game_Manager.isPlaying) return;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (transform.position != target.position)
        {
            transform.position = target.position;
            transform.SetParent(target);
            SetAppear();
        }
    }

    public void SetAppear()
    {
        //Called when collides with a coin
        textCoin.color = newColor;
        dotAnim.DORestart();
    }
}
