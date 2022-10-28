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
        base.DoAbilityEffect();
        StartCoroutine(Disguise());
    }

    private IEnumerator Disguise()
    {
        bool isRunning = true;

        BaseSceneController controller = GameManager.LevelManager.ActiveSceneController;
        controller.enemyAdmin.IsDisguised = true;
        PlayerVirus pv = controller.Player;
        pv.PlayerVisual.SetActive(false);
        trojanVisual.SetActive(true);

        TimeTracker time = new(trojanHorse.AbilityDuration.GetCurrentValue(AbilityUpgrade.UnlockProgression), 1);
        time.onFinish += () =>
        {
            controller.enemyAdmin.IsDisguised = false;
            pv.PlayerVisual.SetActive(true);
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