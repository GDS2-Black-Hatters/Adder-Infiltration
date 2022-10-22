using static VariableManager;

public class Protect : AbilityBase
{
    private DurationAbility protectAbility;
    public override Ability Ability => protectAbility;
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Protect;

    protected override void Awake()
    {
        protectAbility = (DurationAbility)base.Ability;
        base.Awake();
    }

    protected override void DoAbilityEffect()
    {
        print("Alright!");
    }
}
