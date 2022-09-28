using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSensitivitySlider : BaseSlider
{
    [SerializeField] private InputActionReference lookAction;

    protected override void OnValueChanged(float value)
    {
        base.OnValueChanged(value);
        lookAction.action.ApplyParameterOverride("scaleVector2:x", value);
        lookAction.action.ApplyParameterOverride("scaleVector2:y", value * 0.01);
    }
}
