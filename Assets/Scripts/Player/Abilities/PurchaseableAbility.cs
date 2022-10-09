using System;
using UnityEngine;
using static SaveManager.VariableToSave;

[Serializable]
public abstract class PurchaseableAbility : AbilityBase, IPurchaseable
{
    [Serializable]
    protected class Price
    {
        [Min(1)] public int initialUnlockPrice = 100;
        [Min(2)] public int startingPrice = 120;
        [Min(3)] public int endingPrice = 200; //For items with multiple upgrades.

        public int GetPrice(Upgradeable upgrade)
        {
            return upgrade.IsUnlocked ? (int)Mathf.Lerp(startingPrice, endingPrice, upgrade.UnlockProgression) : initialUnlockPrice;
        }
    }

    [field: SerializeField, Header("Text and Labels")] public string Name { get; private set; }
    public string RichDescription
    {
        get
        {
            return $"<align=center><size=16><b>{Name}</b></size></align>\n\n{Description}";
        }
    }

    [SerializeField, TextArea(5, 5)] private string description;
    public string Description
    {
        get
        {
            string desc = description;
            UpgradeDescription(ref desc);

            if (AbilityUpgrade.UnlockProgression != 1)
            {
                IPurchaseable.AppendDescription(ref desc, bytecoins, GetBytecoinPrice, "ByteCoins:\t");
                IPurchaseable.AppendDescription(ref desc, intelligenceData, GetIntelligenceDataPrice, "Intelligence Data:");
                IPurchaseable.AppendDescription(ref desc, processingPower, GetProcessingPowerPrice, "Processing Power:");
            }
            return desc;
        }
    }

    protected virtual void UpgradeDescription(ref string desc)
    {
        desc += "\n\n";
        CalculateNextUpgrade(ref desc, "Cooldown", cooldown, "s");
    }

    protected string CalculateNextUpgrade(ref string desc, string fieldName, LevelValues levelValues, string suffix = "")
    {
        desc += $"{fieldName}: {levelValues.GetCurrentValue(AbilityUpgrade.UnlockProgression)}{suffix}";
        float next = AbilityUpgrade.Level + 1;
        if (AbilityUpgrade.IsUnlocked && next <= AbilityUpgrade.MaxLevel)
        {
            desc += $" -> {levelValues.GetCurrentValue(next / AbilityUpgrade.MaxLevel)}{suffix}";
        }
        desc += "\n";
        return desc;
    }

    [SerializeField, Header("Pricing")] protected Price Bytecoin;
    [SerializeField] protected Price IntelligenceData;
    [SerializeField] protected Price ProcessingPower;
    public int GetBytecoinPrice => Bytecoin.GetPrice(AbilityUpgrade);
    public int GetIntelligenceDataPrice => IntelligenceData.GetPrice(AbilityUpgrade);
    public int GetProcessingPowerPrice => ProcessingPower.GetPrice(AbilityUpgrade);

    public virtual string Label
    {
        get
        {
            string subtitle = "";
            if (AbilityUpgrade.IsUnlocked)
            {
                subtitle = AbilityUpgrade.UnlockProgression == 1 ? "Level Max" : $"Level {AbilityUpgrade.Level + 1}";
            }
            return $"{Name}\n<size=80%>{subtitle}</size>";
        }
    }

    public void UpdatePurchase()
    {
        AbilityUpgrade.Unlock();
    }
}