using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] private List<GameObject> pages = new List<GameObject>();
    [SerializeField] private List<GameObject> pagesButtons = new List<GameObject>();
    [SerializeField] Sprite[] buttonsSprites;

    public void SwitchActiveState(GameObject gameObjectToSwitch)
    {
        bool objectState = gameObjectToSwitch.activeInHierarchy;
        objectState = !objectState;
        gameObjectToSwitch.SetActive(objectState);
    }

    public void SwitchButtonInteractable(Button currentButton)
    {
        bool interactableButton = currentButton.interactable;
        interactableButton = !interactableButton;
        currentButton.interactable = interactableButton;
    }

    public void SwitchDOTAnimation(DOTweenAnimation currentDOT)
    {
        if (!currentDOT.gameObject.activeInHierarchy)
        {
            currentDOT.gameObject.SetActive(true);
            currentDOT.DORestart();
        }
        else
        {
            currentDOT.DOPlayBackwards();
        }
    }

    public void Pages()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(false);
            pagesButtons[i].GetComponent<Image>().sprite = buttonsSprites[0];
            if (EventSystem.current.currentSelectedGameObject == pagesButtons[i])
            {
                pages[i].SetActive(true);
                pagesButtons[i].GetComponent<Image>().sprite = buttonsSprites[1];
            }
        }
    }
}
