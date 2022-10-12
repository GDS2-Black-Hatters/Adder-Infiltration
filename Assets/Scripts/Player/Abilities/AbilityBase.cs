using System;
using System.Collections;
using UnityEngine;
using static VariableManager;

[Serializable]
public abstract class AbilityBase : MonoBehaviour
{
    [Serializable]
    protected class LevelValues
    {
        [SerializeField] private float minLevelValue;
        [SerializeField] private float maxLevelValue;

        public float GetCurrentValue(float percentage)
        {
            return Mathf.Lerp(minLevelValue, maxLevelValue, percentage);
        }
    }

    [field: SerializeField, Header("Base Ability")] public AllAbilities ability { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public Upgradeable AbilityUpgrade { get; protected set; }
    [field: SerializeField] public Upgradeable DefaultUpgrade { get; protected set; } = new(10);

    [SerializeField] protected LevelValues cooldown;

    public bool IsCoolingDown { get; private set; } = false;

    public event Action CooldownUpdate;
    public event Action CooldownFinish;
    public TimeTracker CooldownTimer { get; private set; } = new(1, 1);

    protected virtual void Awake()
    {
        AbilityUpgrade = (Upgradeable)GameManager.VariableManager.GetUnlockable(DoStatic.EnumToEnum<AllAbilities, AllUnlockables>(ability));
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
        CooldownTimer.SetTimer(cooldown.GetCurrentValue(AbilityUpgrade.UnlockProgression));
    }

    protected abstract void DoAbilityEffect();
    public virtual void EndAbilityPrime() { }
    public virtual void StartAbilityPrime() { }
}