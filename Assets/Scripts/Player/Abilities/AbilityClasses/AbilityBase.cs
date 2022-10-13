using System;
using System.Collections;
using UnityEngine;
using static VariableManager;

[Serializable]
public abstract class AbilityBase : MonoBehaviour
{
    public abstract AllAbilities AbilityID { get; protected set; }
    public virtual Ability Ability => GameManager.VariableManager.GetAbility(AbilityID);

    private Upgradeable upgrade; //For perfomance.
    public Upgradeable AbilityUpgrade => upgrade;

    public bool IsCoolingDown { get; private set; } = false;

    public event Action CooldownUpdate;
    public event Action CooldownFinish;
    public TimeTracker CooldownTimer { get; private set; } = new(1, 1);

    protected virtual void Awake()
    {
        upgrade = (Upgradeable)GameManager.VariableManager.GetUnlockable(DoStatic.EnumToEnum<AllAbilities, AllUnlockables>(AbilityID));
        CooldownTimer.onFinish += () =>
        {
            CooldownFinish?.Invoke();
            IsCoolingDown = false;
        };
        CalculateAbilityStats();
    }

    private IEnumerator Cooldown()
    {
        IsCoolingDown = true;
        do
        {
            yield return null;
            CooldownTimer.Update(Time.deltaTime);
            CooldownUpdate?.Invoke();
        } while (CooldownTimer.TimePercentage != 1);
        CooldownTimer.Reset();
    }

    public void ActivateAbility()
    {
        if (!IsCoolingDown)
        {
            StartCoroutine(Cooldown());
            DoAbilityEffect();
        }
    }

    protected virtual void CalculateAbilityStats()
    {
        CooldownTimer.SetTimer(Ability.Cooldown.GetCurrentValue(AbilityUpgrade.UnlockProgression));
    }

    protected abstract void DoAbilityEffect();
}