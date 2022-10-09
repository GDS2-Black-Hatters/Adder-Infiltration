using UnityEngine;

public class PurchaseOnceButton : StoreItemButton
{
    [SerializeField, Header("Settings")] private ShopItem item;

    protected override IPurchaseable GetPurchaseable()
    {
        return item;
    }

    public override bool CanPurchase => !item.Item.IsUnlocked && HasSufficientMoney();
}