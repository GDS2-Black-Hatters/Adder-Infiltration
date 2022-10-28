
using System.Collections;
using UnityEngine;
using static VariableManager;

public class Protect : AbilityBase
{
    private DurationAbility protectAbility;
    public override Ability Ability => protectAbility;
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Protect;
    [SerializeField] private Transform shield;

    protected override void Awake()
    {
        protectAbility = (DurationAbility)base.Ability;
        base.Awake();
    }

    protected override void DoAbilityEffect()
    {
        base.DoAbilityEffect();
        StartCoroutine(ProtectAbility());
    }

    private IEnumerator ProtectAbility()
    {
        PlayerVirus player = GameManager.LevelManager.ActiveSceneController.Player;
        player.SetProtected(true);
        shield.gameObject.SetActive(true);

        TimeTracker time = new(protectAbility.AbilityDuration.GetCurrentValue(AbilityUpgrade.UnlockProgression), 1);
        time.onFinish += () =>
        {
            player.SetProtected(false);
            shield.gameObject.SetActive(false);
        };

        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            shield.position = player.transform.position;
        } while (player.IsProtected);
    }
}
