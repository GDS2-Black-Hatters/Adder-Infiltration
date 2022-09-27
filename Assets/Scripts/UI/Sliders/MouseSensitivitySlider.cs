using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSensitivitySlider : BaseSlider
{
    [SerializeField] private InputActionReference lookAction;

    protected override void Awake()
    {
        base.Awake();
        slider.value = 0.09f; //TODO: Grab the value from save file
    }

    protected override void OnValueChanged(float value)
    {
        lookAction.action.ApplyParameterOverride("scaleVector2:x", value );
        lookAction.action.ApplyParameterOverride("scaleVector2:y", value * 0.01);
    }
}
