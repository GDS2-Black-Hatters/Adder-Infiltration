using System;
using UnityEngine;
using static VariableManager;

[CreateAssetMenu(fileName = "NewAbilityItem", menuName = "ScriptableObject/Shop Item/Ability Item")]
public class AbilityItem : BaseShopItem
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

    [SerializeField] private AllAbilities abilityID;
    [SerializeField] protected Price Bytecoin;
    [SerializeField] protected Price IntelligenceData;
    [SerializeField] protected Price ProcessingPower;

    [NonSerialized] private Upgradeable upgrade;
    public Upgradeable AbilityUpgrade => upgrade ??= (Upgradeable)GameManager.VariableManager.GetUnlockable(DoStatic.EnumToEnum<AllAbilities, AllUnlockables>(abilityID));
    public virtual Ability Ability => GameManager.VariableManager.GetAbility(abilityID);

    public override int GetBytecoinPrice => Bytecoin.GetPrice(AbilityUpgrade);
    public override int GetIntelligenceDataPrice => IntelligenceData.GetPrice(AbilityUpgrade);
    public override int GetProcessingPowerPrice => ProcessingPower.GetPrice(AbilityUpgrade);
    public override string RichDescription
    {
        get
        {
            return $"<align=center><size=16><b>{Name}</b></size></align>\n\n{Description}";
        }
    }

    private string Description
    {
        get
        {
            string desc = description;
            UpgradeDescription(ref desc);
            PriceDescription(AbilityUpgrade.UnlockProgression != 1, ref desc);
            return desc;
        }
    }

    protected virtual void UpgradeDescription(ref string desc)
    {
        desc += "\n\n";
        CalculateNextUpgrade(ref desc, "Cooldown", Ability.Cooldown, "s");
    }

    protected string CalculateNextUpgrade(ref string desc, string fieldName, Ability.LevelValues levelValues, string suffix = "")
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

    public override string Label
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

    public override void UpdatePurchase()
    {
        AbilityUpgrade.Unlock();
    }
}