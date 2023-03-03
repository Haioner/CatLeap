using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InfoButton : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private GameObject infoObject;
    [SerializeField] private DOTweenAnimation infoDOT;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private string upgradeInfo = "This upgrade unlocks more maps!";
    private bool isTouching = false;

    public void ReturnTouching(bool isTouchingState)
    {
        isTouching = isTouchingState;
    }

    private void Update()
    {
        if (isTouching && !infoObject.activeInHierarchy)
        {
            infoText.text = upgradeInfo;
            infoObject.SetActive(true);
            infoDOT.DORestart();
        }

        if(!isTouching && infoObject.activeInHierarchy)
        {
            //infoObject.SetActive(false);
            infoDOT.DOPlayBackwards();
        }
    }
}
