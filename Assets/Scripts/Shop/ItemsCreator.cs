using System.Collections.Generic;
using UnityEngine;

public class ItemsCreator : MonoBehaviour
{
    [SerializeField] private ShopItemsScriptable shopSO;
    [SerializeField] private ItemShop itemShop;
    [SerializeField] private GameObject contentHolder;
    public List<ItemShop> allItems = new List<ItemShop>();

    private void Awake()
    {
        CreateItemsShop();
    }

    private void CreateItemsShop()
    {
        int startItemsCount = allItems.Count == 0 ? startItemsCount = 0 : startItemsCount = allItems.Count;

        for (int i = 0; i < shopSO.itemShopType.Length; i++)
        {
            ItemShop item = Instantiate(itemShop, contentHolder.transform);
            item.SetValues
                (shopSO.itemShopType[i].price, //Price
                shopSO.itemShopType[i].itemSprite[0], //NoSelectedImage
                shopSO.itemShopType[i].itemSprite[1], //OutlineImage
                i + startItemsCount, //ID
                shopSO.itemShopType[i].itemName, //ItemName
                shopSO.itemShopType[i].spriteLibrary); //SpriteLibraryAsset

            allItems.Add(item);
            item.CheckPurchased();
            item.CheckUsed();
        }
    }

    public void SetOutlines(float m_id)
    {
        //Change outlines items (Use one)
        for (int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].id == m_id)
                allItems[i].OutlineState(1);
            else
                allItems[i].OutlineState(0);
        }
    }

}
