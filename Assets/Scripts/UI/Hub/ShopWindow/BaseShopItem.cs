using UnityEngine;
using static SaveManager;
using static SaveManager.VariableToSave;

public abstract class BaseShopItem : BaseItem
{
    [field: SerializeField] public override Sprite Icon { get; protected set; }
    [field: SerializeField, Header("Basic Info")] public string Name { get; protected set; }
    [field: SerializeField, TextArea(5, 5)] protected string description;
    
    public abstract int GetBytecoinPrice { get; }
    public abstract int GetIntelligenceDataPrice { get; }
    public abstract int GetProcessingPowerPrice { get; }
    public abstract string RichDescription { get; }
    public abstract void UpdatePurchase();

    protected void PriceDescription(bool add, ref string rich)
    {
        if (add)
        {
            AppendDescription(ref rich, bytecoins, GetBytecoinPrice, "ByteCoins:\t");
            AppendDescription(ref rich, intelligenceData, GetIntelligenceDataPrice, "Intelligence Data:");
            AppendDescription(ref rich, processingPower, GetProcessingPowerPrice, "Processing Power:");
        }
    }

    private void AppendDescription(ref string rich, VariableToSave currency, int cost, string variableName)
    {
        string prefix = "";
        string suffix = $"{cost}";
        if (GameManager.VariableManager.GetVariable<int>(currency) < cost)
        {
            prefix = $"<color=\"red\">";
            suffix += $"</color>";
        }

        rich += $"\n{prefix}{variableName}\t\t{suffix}";
    }
}
