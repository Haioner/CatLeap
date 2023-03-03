using UnityEngine.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems", menuName = "ScriptableObjects/ShopItems_SO", order = 1)]
public class ShopItemsScriptable : ScriptableObject
{
    [System.Serializable]
    public struct ItemShop
    {
        public float price;
        public string itemName;
        public Sprite[] itemSprite;
        public SpriteLibraryAsset spriteLibrary;
    }

    public ItemShop[] itemShopType;
}
