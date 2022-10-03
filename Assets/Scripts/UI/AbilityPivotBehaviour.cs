using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPivotBehaviour : MonoBehaviour
{
    [SerializeField] private Image frame;
    [SerializeField] private Image icon;
    public bool IsCoolingDown { get; private set; } = false;

    public void ActivateAbility()
    {
        if (!IsCoolingDown && !AbilityWheelBehaviour.IsRotating)
        {
            //TODO: Call ability effect here.
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        IsCoolingDown = true;
        TimeTracker time = new(1, 1); //TODO: Hook up to ability's cooldown value.
        time.onFinish += () => { IsCoolingDown = false; };
        icon.color = Color.gray;
        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            frame.fillAmount = time.TimePercentage;
        } while (IsCoolingDown);
        icon.color = Color.white;
    }
}
