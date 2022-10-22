using static VariableManager;

public class Heal : AbilityBase
{
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Heal;

    protected override void DoAbilityEffect()
    {
        print("Alright!");
    }
}
