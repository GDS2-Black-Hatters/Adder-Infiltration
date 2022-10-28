using System.Collections;
using UnityEngine;
using static VariableManager;

public class EMP : AbilityBase
{
    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.EMP;

    [SerializeField] private Transform chargeSphere;
    [SerializeField] private Transform effectSphere;
    private EMPAbility emp;
    public override Ability Ability => emp;

    protected override void Awake()
    {
        emp = (EMPAbility)base.Ability;
        base.Awake();
        chargeSphere.gameObject.SetActive(false);
        effectSphere.gameObject.SetActive(false);
    }

    protected override void DoAbilityEffect()
    {
        base.DoAbilityEffect();
        StartCoroutine(EmpEffect());
    }

    public float GetStunDuration()
    {
        return emp.StunDuration.GetCurrentValue(AbilityUpgrade.UnlockProgression);
    }

    private IEnumerator EmpEffect()
    {
        IEnumerator Explosion(float duration, Transform sphere, float maxAngle)
        {
            bool isRunning = true;
            TimeTracker time = new(duration, 1);
            time.onFinish += () =>
            {
                isRunning = false;
            };

            do
            {
                yield return null;
                time.Update(Time.deltaTime);
                sphere.position = GameManager.LevelManager.ActiveSceneController.Player.transform.position;
                float size = Mathf.Sin(Mathf.Lerp(0, maxAngle, time.TimePercentage) * Mathf.Deg2Rad);
                sphere.localScale = emp.EffectRange.GetCurrentValue(AbilityUpgrade.UnlockProgression) * size * Vector3.one;
            } while (isRunning);
        }

        //Charge up effect
        float chargeSpeed = emp.ChargeUpTime.GetCurrentValue(AbilityUpgrade.UnlockProgression);
        if (chargeSpeed != 0)
        {
            chargeSphere.gameObject.SetActive(true);
            yield return StartCoroutine(Explosion(emp.ChargeUpTime.GetCurrentValue(AbilityUpgrade.UnlockProgression), chargeSphere, 180));
            chargeSphere.gameObject.SetActive(false);
        }

        //Explosion in
        effectSphere.gameObject.SetActive(true);
        yield return StartCoroutine(Explosion(0.3f, effectSphere, 90));

        //Explosion out
        yield return new WaitForSeconds(3);
        effectSphere.gameObject.SetActive(false);
    }
}
