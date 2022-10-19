using UnityEngine;
using static VariableManager;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObject/Shop Item/Item")]
public class ShopItem : BaseShopItem
{
    [SerializeField] private AllUnlockables unlockable;
    public Unlockable Unlock => GameManager.VariableManager.GetUnlockable(unlockable);

    public override string Label
    {
        get
        {
            string purchased = Unlock.IsUnlocked ? "Purchased" : "";
            return $"{Name}\n<size=80%>{purchased}</size>";
        }
        protected set => Label = value;
    }

    public override string RichDescription
    {
        get
        {
            string itemRichDesc = $"<align=center><size=16><b>{Name}</b></size></align>\n\n{description}\n";
            PriceDescription(!Unlock.IsUnlocked, ref itemRichDesc);
            return itemRichDesc;
        }
    }


    [SerializeField, Header("Pricing"), Min(0)] private int Bytecoin;
    [SerializeField, Min(0)] private int IntelligenceData;
    [SerializeField, Min(0)] private int ProcessingPower;
    public override int GetBytecoinPrice => Bytecoin;
    public override int GetIntelligenceDataPrice => IntelligenceData;
    public override int GetProcessingPowerPrice => ProcessingPower;

    public override void UpdatePurchase()
    {
        Unlock.Unlock();
    }
}
