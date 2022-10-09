using UnityEngine;

public class AbilityUpgradeButton : StoreItemButton
{
    [SerializeField, Header("Settings")] private VariableManager.AllAbilities abilityID;
    private PurchaseableAbility ability;

    protected override void Awake()
    {
        ability = (PurchaseableAbility)GameManager.VariableManager.GetAbility(abilityID);
        base.Awake();
    }

    protected override IPurchaseable GetPurchaseable()
    {
        return ability;
    }

    public override bool CanPurchase => ability.AbilityUpgrade.UnlockProgression != 1 && HasSufficientMoney();
}