using System.Collections.Generic;
using UnityEngine;

public class AdderAssetOptimizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> allObjectsToAdd = new List<GameObject>();
    [SerializeField] private AssetsGraphicState assetsGraphicState;
    [SerializeField] private bool isToDisableOnAwake = true;

    private void Awake()
    {
        AddAllObjectsToGraphicState();
    }
    
    private void AddAllObjectsToGraphicState()
    {
        for (int i = 0; i < allObjectsToAdd.Count; i++)
        {
            assetsGraphicState.AddObjectoToList(allObjectsToAdd[i]);
        }
        if (isToDisableOnAwake)
            this.gameObject.SetActive(false);
    }
}
