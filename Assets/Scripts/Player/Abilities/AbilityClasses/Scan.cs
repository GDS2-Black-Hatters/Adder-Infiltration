using UnityEngine;
using static VariableManager;

public class Scan : AbilityBase
{
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Scan;

    protected override void DoAbilityEffect()
    {
        Time.timeScale = 0.5f;
    }


}
