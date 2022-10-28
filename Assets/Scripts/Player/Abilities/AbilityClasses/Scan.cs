using static VariableManager;

public class Scan : AbilityBase
{
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Scan;

    protected override void DoAbilityEffect()
    {
        base.DoAbilityEffect();
        GameManager.LevelManager.ActiveSceneController.StartScan();
    }
}
