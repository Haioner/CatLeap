using System.Collections.Generic;
using UnityEngine;

public class AssetsGraphicState : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsListSetActive;
    private int currentQuality;

    public void AddObjectoToList(GameObject m_object)
    {
        objectsListSetActive.Add(m_object);
    }

    public void CheckGraphicsState(int qualityValue)
    {
        currentQuality = qualityValue;
        switch (currentQuality)
        {
            case 2: //DISABLE
                for (int i = 0; i < objectsListSetActive.Count; i++)
                {
                    objectsListSetActive[i].SetActive(false);
                }
                break;

            default: //ENABLE
                for (int i = 0; i < objectsListSetActive.Count; i++)
                {
                    objectsListSetActive[i].SetActive(true);
                }
                break;
        }
    }
}
