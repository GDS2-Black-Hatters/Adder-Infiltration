using System.Collections;
using UnityEngine;
using static VariableManager;

public class Heal : AbilityBase
{
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Heal;
    [SerializeField] private GameObject healParticle;

    protected override void DoAbilityEffect()
    {
        base.DoAbilityEffect();
        StartCoroutine(HealAbility());
    }

    public IEnumerator HealAbility()
    {
        PlayerVirus player = GameManager.LevelManager.ActiveSceneController.Player;
        player.Heal(0.5f);
        TimeTracker time = new(1);
        healParticle.SetActive(true);
        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            healParticle.transform.position = player.transform.position;
        } while (time.TimePercentage != 0);
        healParticle.SetActive(false);
    }
}
