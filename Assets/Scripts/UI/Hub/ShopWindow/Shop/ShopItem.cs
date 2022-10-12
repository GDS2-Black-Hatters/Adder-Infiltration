using UnityEngine;
using static SaveManager.VariableToSave;
using static VariableManager;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObject/Shop Item/Item")]
public class ShopItem : ScriptableObject, IPurchaseable
{
    [SerializeField] private AllUnlockables unlockable;
    public Unlockable Item => GameManager.VariableManager.GetUnlockable(unlockable);

    [field: SerializeField, Header("Text and Labels")] public string Name { get; private set; }
    [field: SerializeField, TextArea(5, 5)] private string description;

    [field: SerializeField, Header("Price")] public int GetBytecoinPrice { get; private set; }
    [field: SerializeField] public int GetIntelligenceDataPrice { get; private set; }
    [field: SerializeField] public int GetProcessingPowerPrice { get; private set; }

    public virtual string Label
    {
        get
        {
            string purchased = Item.IsUnlocked ? "Purchased" : "";
            return $"{Name}\n<size=80%>{purchased}</size>";
        }
    }

    public string RichDescription
    {
        get
        {
            string itemRichDesc = $"<align=center><size=16><b>{Name}</b></size></align>\n\n{description}";
            if (!Item.IsUnlocked)
            {
                IPurchaseable.AppendDescription(ref itemRichDesc, bytecoins, GetBytecoinPrice, "\nByteCoins:\t");
                IPurchaseable.AppendDescription(ref itemRichDesc, intelligenceData, GetIntelligenceDataPrice, "Intelligence Data:");
                IPurchaseable.AppendDescription(ref itemRichDesc, processingPower, GetProcessingPowerPrice, "Processing Power:");
            }
            return itemRichDesc;
        }
    }

    public void UpdatePurchase()
    {
        Item.Unlock();
    }
}
