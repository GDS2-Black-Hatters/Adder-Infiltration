using System.Collections;
using UnityEngine;

public class TrojanHorse : PurchaseableAbility
{
    [SerializeField] private LevelValues abilityDuration;
    [SerializeField] private GameObject trojinVisual;

    protected override void UpgradeDescription(ref string desc)
    {
        base.UpgradeDescription(ref desc);
        CalculateNextUpgrade(ref desc, "Duration", abilityDuration, "s");
    }

    protected override void DoAbilityEffect()
    {
        PlayerVirus pv = GameManager.LevelManager.player;
        pv.gameObject.layer = LayerMask.NameToLayer("Default");
        GameManager.LevelManager.player.tag = "Untagged";

        pv.playerVisual.SetActive(false);
        trojinVisual.SetActive(true);
        StartCoroutine(DelayDisableAbility());
    }

    private IEnumerator DelayDisableAbility()
    {
        TimeTracker time = new(abilityDuration.GetCurrentValue(AbilityUpgrade.UnlockProgression), 1);
        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            trojinVisual.transform.position = GameManager.LevelManager.player.transform.position;
        } while (time.TimePercentage != 1);

        GameManager.LevelManager.player.gameObject.layer = LayerMask.NameToLayer("Player");
        GameManager.LevelManager.player.tag = "Player";
        GameManager.LevelManager.player.playerVisual.SetActive(true);
        trojinVisual.SetActive(false);
    }
}
