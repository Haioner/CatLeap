using UnityEngine;
using DG.Tweening;
using TMPro;

public class UpgradeMaps : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private Maps maps;
    [SerializeField] private int countMapsToUnlock = 1;
    [SerializeField] private float price = 100f;
    [SerializeField] private int maxUpgradeLevel;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI upgradeLevelText;
    [SerializeField] private GameObject insuficientCoinsText;
    [SerializeField] private DOTweenAnimation insufficientDOT;

    private void OnEnable()
    {
        LoadMaxUpgradeLevel();
        LoadPriceAndLevel();
    }

    private void LoadMaxUpgradeLevel()
    {
        maxUpgradeLevel = maps.mapsList.Count - maps.maxMap;
    }

    public void UpgradeMaxMap()
    {
        if (maxUpgradeLevel <= 0)
        {
            CheckMaxUpgrade();
            return;
        }

        if (price > Game_Manager.instance.coins)
        {
            CheckInsuficientCoins();
            return;
        }
        maps.AddSaveMaxMap(countMapsToUnlock);
        UpdatePriceAndLevel();
    }

    private void CheckMaxUpgrade()
    {
        priceText.text = "MAX";
        upgradeLevelText.text = (maps.maxMap - 1).ToString() + "/" + (maps.mapsList.Count - 1).ToString() + "(MAX)";
    }

    private void CheckInsuficientCoins()
    {
        insuficientCoinsText.SetActive(true);
        insufficientDOT.DORestart();
    }

    private void UpdatePriceAndLevel()
    {
        //Price
        Game_Manager.instance.RemoveCoin(price);
        price = price * 1.5f;
        PlayerPrefs.SetFloat("upgrademaps_price", price);

        LoadMaxUpgradeLevel();
        UpdateTexts();
    }

    private void LoadPriceAndLevel()
    {
        //Price
        if (PlayerPrefs.HasKey("upgrademaps_price"))
            price = PlayerPrefs.GetFloat("upgrademaps_price");

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        priceText.text = "<sprite=0>" + FormaterNumber.FormatNumber(price);
        if (maxUpgradeLevel <= 0)
        {
            CheckMaxUpgrade();
            return;
        }
        upgradeLevelText.text = (maps.maxMap - 1).ToString() + "/" + (maps.mapsList.Count - 1).ToString();
    }
}
