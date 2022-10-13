using UnityEngine;
using UnityEngine.UI;

public class AbilityPivotBehaviour : MonoBehaviour
{
    [SerializeField] private Image frame;
    [SerializeField] private Image icon;
    private AbilityBase current;

    public void UpdateAppearance(AbilityBase ability)
    {
        icon.sprite = ability.Ability.Icon;
        OnDisable();
        current = ability;
        current.CooldownUpdate += UpdateCooldown;
        current.CooldownFinish += FinishCooldown;

        if (current.IsCoolingDown)
        {
            UpdateCooldown();
        }
        else
        {
            FinishCooldown();
        }
    }

    private void UpdateCooldown()
    {
        icon.color = Color.gray;
        frame.fillAmount = current.CooldownTimer.TimePercentage;
    }

    private void FinishCooldown()
    {
        icon.color = Color.white;
        frame.fillAmount = 1;
    }

    private void OnDisable()
    {
        if (current)
        {
            current.CooldownUpdate -= UpdateCooldown;
            current.CooldownFinish -= FinishCooldown;
        }
    }
}
