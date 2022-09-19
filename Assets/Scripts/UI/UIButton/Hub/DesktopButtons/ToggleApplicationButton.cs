using UnityEngine;

public class ToggleApplicationButton : BaseDesktopButton
{
    [SerializeField] private DesktopWindowApplication windowApplication;

    protected override void OnClick()
    {
        windowApplication.ToggleApplication();
    }
}
