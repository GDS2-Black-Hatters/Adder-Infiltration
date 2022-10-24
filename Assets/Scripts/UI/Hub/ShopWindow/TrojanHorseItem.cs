using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrojanHorseItem", menuName = "ScriptableObject/Shop Item/Ability Specific Item/Trojan Horse Item")]
public class TrojanHorseItem : AbilityItem
{
    [NonSerialized] private TrojanHorseAbility horseAbility;
    public override Ability Ability => horseAbility = horseAbility ? horseAbility : (TrojanHorseAbility)base.Ability;

    protected override void UpgradeDescription(ref string desc)
    {
        base.UpgradeDescription(ref desc);
        CalculateNextUpgrade(ref desc, "Duration", horseAbility.AbilityDuration, "s");
    }
}
