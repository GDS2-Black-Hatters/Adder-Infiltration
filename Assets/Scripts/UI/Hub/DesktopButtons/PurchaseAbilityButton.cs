public class PurchaseAbilityButton : ShopItemButton
{
    private AbilityItem ability;

    public override void StartUp(BaseButtonContainer container, BaseItem item)
    {
        base.StartUp(container, item);
        ability = (AbilityItem)item;
    }

    public override bool CanPurchase => ability.AbilityUpgrade.UnlockProgression != 1 && HasSufficientMoney();
}