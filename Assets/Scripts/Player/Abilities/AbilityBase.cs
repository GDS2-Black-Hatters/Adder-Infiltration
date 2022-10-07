using System;
using System.Collections;
using UnityEngine;
using static VariableManager;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField, Header("Ability Base")] private AllAbilities ability;
    [field: SerializeField] public Sprite Logo { get; private set; }
    protected Upgradeable abilityUpgrade;
    public TimeTracker CooldownTimer { get; private set; } = new(1, 1);
    public bool IsCoolingDown { get; private set; } = false;

    public event Action CooldownUpdate;
    public event Action CooldownFinish;

    [SerializeField, Header("Ability Stats")]
    private Lerper cooldown = new();

    protected virtual void Start()
    {
        abilityUpgrade = (Upgradeable)GameManager.VariableManager.GetUnlockable(DoStatic.EnumToEnum<AllAbilities, AllUnlockables>(ability));
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
        CooldownFinish?.Invoke();
        IsCoolingDown = false;
    }

    protected virtual void CalculateAbilityStats()
    {
        CooldownTimer.SetTimer(cooldown.ValueAtPercentage(abilityUpgrade.UnlockProgression));
    }

    protected abstract void DoAbilityEffect();

    public void ActivateAbility()
    {
        if (!IsCoolingDown)
        {
            StartCoroutine(Cooldown());
            DoAbilityEffect();
        }
    }
    public abstract void StartAbilityPrime();
    public abstract void EndAbilityPrime();
}
