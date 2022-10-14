public class PurchaseItemButton : ShopItemButton
{
    private ShopItem shopItem;

    public override void StartUp(BaseButtonContainer container, BaseItem item)
    {
        shopItem = (ShopItem)item;
        base.StartUp(container, item);
    }

    public override bool CanPurchase => !shopItem.Unlock.IsUnlocked && HasSufficientMoney();
}