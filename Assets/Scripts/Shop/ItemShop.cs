using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject holderPrice;
    [SerializeField] private Purchase purchaseCanvas;
    [SerializeField] private Sprite[] icons; //0 noSelected //1 selected
    [SerializeField] private Image image;
    private string currentUsed;
    private float price;
    public int id;
    private string itemName;
    private SpriteLibraryAsset spriteAssetLibrary;
    [HideInInspector] public bool isPurchased = false;

    private void Start()
    {
        CheckInitialItem();
    }

    public void SetValues(float _price, Sprite _noSelectedIcon, Sprite _selectedIcon, int _id, string _itemName, SpriteLibraryAsset _spriteLibraryAsset)
    {
        //Set values when created item shop
        price = _price;
        priceText.text = "<sprite=0>" + price.ToString();
        image.sprite = _noSelectedIcon;
        icons[0] = _noSelectedIcon;
        icons[1] = _selectedIcon;
        id = _id;
        itemName = _itemName;
        spriteAssetLibrary = _spriteLibraryAsset;
    }

    private void CheckInitialItem()
    {
        //Free default skin
        if(!PlayerPrefs.HasKey("default"))
        {
            PlayerPrefs.SetString("default", "default");
            PlayerPrefs.SetString("currentUsed", "default");
            CheckPurchased();
            if (itemName == "default")
                Use();
        }
    }

    public void CheckPurchased()
    {
        //Check on start if item is purchased
        if (PlayerPrefs.HasKey(itemName))
        {
            isPurchased = true;
            holderPrice.SetActive(false);
        }
    }

    public void CheckUsed()
    {
        //Load the skin in use
        if (PlayerPrefs.HasKey("currentUsed"))
            currentUsed = PlayerPrefs.GetString("currentUsed");

        if (currentUsed == itemName)
            Use();
    }

    public void Use()
    {
        //Use skin
        if (!isPurchased) return;
        FindObjectOfType<ItemsCreator>().SetOutlines(id);
        currentUsed = itemName;
        PlayerPrefs.SetString("currentUsed", currentUsed);
        FindObjectOfType<SkinController>().SetSkin(spriteAssetLibrary);
    }

    public void OutlineState(int outlineState)
    {
        //Set outline state
        if (outlineState == 0)
            image.sprite = icons[0];
        else
            image.sprite = icons[1];
    }

    public void StartPurchase()
    {
        if (isPurchased) return;
        Purchase purchase = Instantiate(purchaseCanvas, transform.position, Quaternion.identity);
        purchase.SetValues(price, image, itemName, this);
    }
}
