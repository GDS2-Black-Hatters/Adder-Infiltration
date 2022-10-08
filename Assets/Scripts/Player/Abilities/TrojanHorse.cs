using System.Collections;
using UnityEngine;

public class TrojanHorse : AbilityBase
{
    [SerializeField] private Lerper abilityDuration = new();
    [SerializeField] private GameObject trojinVisual;

    private readonly Mesh originalCoreMesh;

    private bool isAbilityActive = false;

    protected override void DoAbilityEffect()
    {
        if (isAbilityActive)
        {
            return;
        }

        PlayerVirus pv = GameManager.LevelManager.player;
        pv.gameObject.layer = LayerMask.NameToLayer("Default");
        GameManager.LevelManager.player.tag = "Untagged";

        pv.playerVisual.SetActive(false);
        trojinVisual.SetActive(true);

        isAbilityActive = true;
        StartCoroutine(DelayDisableAbility());
    }

    public override void EndAbilityPrime()
    {
    }

    public override void StartAbilityPrime()
    {
    }

    private IEnumerator DelayDisableAbility()
    {
        yield return new WaitForSeconds(abilityDuration.ValueAtPercentage(abilityUpgrade.UnlockProgression));
        GameManager.LevelManager.player.gameObject.layer = LayerMask.NameToLayer("Player");
        GameManager.LevelManager.player.tag = "Player";
        GameManager.LevelManager.player.playerVisual.SetActive(true);
        trojinVisual.SetActive(false);
        isAbilityActive = false;
    }
}
