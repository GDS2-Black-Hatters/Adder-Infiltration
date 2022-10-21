using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDurationAbilityItem", menuName = "ScriptableObject/Shop Item/Ability Specific Item/Duration Ability Item")]
public class DurationAbilityItem : AbilityItem
{
    [NonSerialized] private DurationAbility durationAbility;
    public override Ability Ability => durationAbility = durationAbility ? durationAbility : (DurationAbility)base.Ability;

    protected override void UpgradeDescription(ref string desc)
    {
        base.UpgradeDescription(ref desc);
        CalculateNextUpgrade(ref desc, "Duration", durationAbility.AbilityDuration, "s");
    }
}
