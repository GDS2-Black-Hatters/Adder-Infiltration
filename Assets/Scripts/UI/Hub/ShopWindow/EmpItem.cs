using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrojanHorseItem", menuName = "ScriptableObject/Shop Item/Ability Specific Item/Trojan Horse Item")]
public class EmpItem : AbilityItem
{
    [NonSerialized] private EMPAbility empAbility;
    public override Ability Ability => empAbility = empAbility ? empAbility : (EMPAbility)base.Ability;

    protected override void UpgradeDescription(ref string desc)
    {
        base.UpgradeDescription(ref desc);
        CalculateNextUpgrade(ref desc, "Charge Time", empAbility.ChargeUpTime, "s");
        CalculateNextUpgrade(ref desc, "Effect Range", empAbility.EffectRange);
        CalculateNextUpgrade(ref desc, "Stun Duration", empAbility.StunDuration, "s");
    }
}
