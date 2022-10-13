using System.Collections;
using UnityEngine;
using static VariableManager;

public class TrojanHorse : AbilityBase
{    
    [SerializeField] private GameObject trojinVisual;
    private TrojanHorseAbility trojanHorse;
    public override Ability Ability => trojanHorse;
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.TrojanHorse;

    protected override void Awake()
    {
        trojanHorse = (TrojanHorseAbility)base.Ability;
        base.Awake();
    }

    protected override void DoAbilityEffect()
    {
        PlayerVirus pv = GameManager.LevelManager.ActiveSceneController.player;
        pv.gameObject.layer = LayerMask.NameToLayer("Default");
        pv.tag = "Untagged";

        pv.playerVisual.SetActive(false);
        trojinVisual.SetActive(true);
        StartCoroutine(DelayDisableAbility());
    }

    private IEnumerator DelayDisableAbility()
    {
        PlayerVirus pv = GameManager.LevelManager.ActiveSceneController.player;
        TimeTracker time = new(trojanHorse.AbilityDuration.GetCurrentValue(AbilityUpgrade.UnlockProgression), 1);
        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            trojinVisual.transform.position = pv.transform.position;
        } while (time.TimePercentage != 1);

        pv.gameObject.layer = LayerMask.NameToLayer("Player");
        pv.tag = "Player";
        pv.playerVisual.SetActive(true);
        trojinVisual.SetActive(false);
    }
}
