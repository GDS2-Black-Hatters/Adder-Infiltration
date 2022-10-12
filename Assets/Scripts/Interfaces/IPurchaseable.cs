using static SaveManager;

public interface IPurchaseable
{
    public string Name { get; }
    public string Label { get; }
    public string RichDescription { get; }
    public int GetBytecoinPrice { get; }
    public int GetIntelligenceDataPrice { get; }
    public int GetProcessingPowerPrice { get; }
    public void UpdatePurchase();

    public static void AppendDescription(ref string rich, VariableToSave currency, int cost, string variableName)
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
