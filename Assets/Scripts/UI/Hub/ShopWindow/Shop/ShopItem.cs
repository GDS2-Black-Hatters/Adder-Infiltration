using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObject/Shop Item/Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] private VariableManager.AllUnlockables unlockable = VariableManager.AllUnlockables.Dash;
    public Unlockable Item => GameManager.VariableManager.GetUnlockable(unlockable);

    [Serializable]
    protected class Price
    {
        [Min(0)] public int startingPrice = 0;
        [Min(0)] public int endingPrice = 100; //For items with multiple upgrades.
    }

    [field: SerializeField, Header("Text and Labels")] public string ItemName { get; private set; }
    [TextArea(5, 5)] public string description;
    public virtual string Label => ItemName;
    public virtual string Description => description;

    [Header("Price")]
    [SerializeField] protected Price bytecoin;
    [SerializeField] protected Price intelligenceData;
    [SerializeField] protected Price processingPower;
    public int BytecoinPrice => GetPrice(bytecoin);
    public int IntelligenceDataPrice => GetPrice(intelligenceData);
    public int ProcessingPowerPrice => GetPrice(processingPower);
    protected virtual int GetPrice(Price price) { return price.startingPrice; }

    public void Purchase() { Item.Unlock(); }
}
