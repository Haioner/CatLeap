using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Purchase : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject warnText;
    private ItemShop currentItem;
    private string saveName;
    private float price;

    public void SetValues(float _price, Image _image, string _saveName, ItemShop item)
    {
        price = _price;
        priceText.text = "<sprite=0>" + _price;
        image.sprite = _image.sprite;
        saveName = _saveName;
        currentItem = item;
    }

    public void PurchaseItem()
    {
        if (Game_Manager.instance.coins >= price)
        {
            SavePurchase();
            Game_Manager.instance.RemoveCoin(price);
            gameObject.SetActive(false);
            currentItem.CheckPurchased();
            currentItem.Use();
        }
        else
        {
            warnText.SetActive(true);
        }
    }

    public void CancelPurchase()
    {
        Destroy(this.gameObject);
    }

    public void SavePurchase()
    {
        PlayerPrefs.SetString(saveName, saveName);
    }
}
