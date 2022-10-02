using UnityEngine.InputSystem;
using static InputManager;

public class MouseSensitivitySlider : BaseSlider
{
    protected override void OnValueChanged(float value)
    {
        base.OnValueChanged(value);
        MainGameLook.ApplyParameterOverride("scaleVector2:x", value);
        MainGameLook.ApplyParameterOverride("scaleVector2:y", value * 0.01);
    }
}
