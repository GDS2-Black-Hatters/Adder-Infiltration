using UnityEngine;
using static VariableManager;

public class Dash : AbilityBase
{
    [SerializeField] private float strength = 300f;

    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Dash;

    protected override void DoAbilityEffect()
    {
        base.DoAbilityEffect();
        GameManager.LevelManager.ActiveSceneController.Player.Dash(strength);
    }
}
