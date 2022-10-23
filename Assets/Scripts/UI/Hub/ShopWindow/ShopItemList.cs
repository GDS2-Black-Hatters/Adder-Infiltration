using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItemList", menuName = "ScriptableObject/Shop Item/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] private AbilityItem[] abilities;
    [SerializeField] private ShopItem[] minigames;
    [SerializeField] private ShopItem[] mouses;
    [SerializeField] private ShopItem[] backgrounds;
    [NonSerialized] private List<BaseShopItem> items;

    public List<BaseShopItem> GetItems()
    {
        if (items == null)
        {
            items = new();
            items.AddRange(abilities);
            items.AddRange(minigames);
            items.AddRange(mouses);
            items.AddRange(backgrounds);
        }
        return items;
    }
}
