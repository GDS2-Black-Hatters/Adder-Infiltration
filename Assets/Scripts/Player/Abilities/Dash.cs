using UnityEngine;

public class Dash : PurchaseableAbility
{
    [SerializeField, Header("Dash Settings")] private float strength = 300f;

    protected override void DoAbilityEffect()
    {
        GameManager.LevelManager.player.Dash(strength);
    }
}
