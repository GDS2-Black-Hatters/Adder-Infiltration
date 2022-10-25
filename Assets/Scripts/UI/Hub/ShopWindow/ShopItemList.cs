using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItemList", menuName = "ScriptableObject/Shop Item/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] private AbilityItem[] abilities;
    [SerializeField] private ShopItem[] minigames;
    [SerializeField] private ShopItem[] mouses;

    public List<BaseShopItem> GetItems()
    {
        List<BaseShopItem> items = new();
        items.AddRange(abilities);
        items.AddRange(minigames);
        items.AddRange(mouses);
        return items;
    }
}
