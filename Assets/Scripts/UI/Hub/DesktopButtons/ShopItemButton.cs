using static SaveManager.VariableToSave;

public abstract class ShopItemButton : BaseItemButton
{
    protected BaseShopItem Item;

    public override void StartUp(BaseButtonContainer container, BaseItem item)
    {
        base.StartUp(container, item);
        Item = (BaseShopItem)item;
    }

    public string RichDescription => Item.RichDescription;
    public abstract bool CanPurchase { get; }
    protected bool HasSufficientMoney()
    {
        VariableManager var = GameManager.VariableManager;
        bool sufficientMoney = var.GetVariable<int>(bytecoins) >= Item.GetBytecoinPrice
            && var.GetVariable<int>(intelligenceData) >= Item.GetIntelligenceDataPrice
            && var.GetVariable<int>(processingPower) >= Item.GetProcessingPowerPrice;
        return sufficientMoney;
    }

    public void Purchase()
    {
        GameManager.VariableManager.Purchase(Item.GetBytecoinPrice, Item.GetIntelligenceDataPrice, Item.GetProcessingPowerPrice);
        Item.UpdatePurchase();
        GameManager.VariableManager.InvokePurchase();
        GameManager.SaveManager.SaveToFile(false);
        UpdateValues();
    }
}
