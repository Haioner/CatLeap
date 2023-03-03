using UnityEngine;
using TMPro;

public class CoinGain : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinGainText;

    public void SetCoinGainText(float value)
    {
        coinGainText.text = "+" + FormaterNumber.FormatNumber(value).ToString();
    }

    public void DestroyOnTimer()
    {
        Destroy(this.gameObject, 0.3f);
    }

}
