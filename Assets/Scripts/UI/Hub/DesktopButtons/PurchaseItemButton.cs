public class PurchaseItemButton : ShopItemButton
{
    private ShopItem shopItem;

    public override void StartUp(BaseShopItem item)
    {
        shopItem = (ShopItem)item;
        base.StartUp(item);
    }

    public override bool CanPurchase => !shopItem.Unlock.IsUnlocked && HasSufficientMoney();
}