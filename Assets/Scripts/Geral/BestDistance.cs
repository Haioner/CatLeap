using UnityEngine;
using TMPro;

public class BestDistance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI meterText;

    private void Start()
    {
        meterText.text = FormaterNumber.FormatDistance(Game_Manager.instance.LoadBestDistance());
    }

}
