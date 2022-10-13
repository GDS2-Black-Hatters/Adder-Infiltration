public class PurchaseAbilityButton : ShopItemButton
{
    private AbilityItem ability;

    public override void StartUp(BaseShopItem item)
    {
        ability = (AbilityItem)item;
        base.StartUp(item);
    }

    public override bool CanPurchase => ability.AbilityUpgrade.UnlockProgression != 1 && HasSufficientMoney();
}