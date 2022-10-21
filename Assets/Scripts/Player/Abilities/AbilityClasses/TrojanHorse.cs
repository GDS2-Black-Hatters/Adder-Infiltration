using System.Collections;
using UnityEngine;
using static VariableManager;

public class TrojanHorse : AbilityBase
{    
    [SerializeField] private GameObject trojanVisual;
    private DurationAbility trojanHorse;
    public override Ability Ability => trojanHorse;
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.TrojanHorse;

    protected override void Awake()
    {
        trojanHorse = (DurationAbility)base.Ability;
        base.Awake();
    }

    protected override void DoAbilityEffect()
    {
        PlayerVirus pv = GameManager.LevelManager.ActiveSceneController.Player;
        pv.gameObject.layer = LayerMask.NameToLayer("Default");
        pv.tag = "Untagged";

        pv.playerVisual.SetActive(false);
        trojanVisual.SetActive(true);
        StartCoroutine(DelayDisableAbility());
    }

    private IEnumerator DelayDisableAbility()
    {
        bool isRunning = true;

        PlayerVirus pv = GameManager.LevelManager.ActiveSceneController.Player;
        TimeTracker time = new(trojanHorse.AbilityDuration.GetCurrentValue(AbilityUpgrade.UnlockProgression), 1);
        time.onFinish += () =>
        {
            pv.gameObject.layer = LayerMask.NameToLayer("Player");
            pv.tag = "Player";
            pv.playerVisual.SetActive(true);
            trojanVisual.SetActive(false);
            isRunning = false;
        };
        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            trojanVisual.transform.position = pv.transform.position;
        } while (isRunning);
    }
}