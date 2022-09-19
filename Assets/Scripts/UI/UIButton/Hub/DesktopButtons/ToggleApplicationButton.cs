using UnityEngine;

public class ToggleApplicationButton : BaseDesktopButton
{
    [SerializeField] private DesktopWindowApplication windowApplication;

    private void Start()
    {
        windowApplication.SetStartPos(transform.position); //For some reason, this only works in Start()
    }

    protected override void OnClick()
    {
        windowApplication.ToggleApplication();
    }
}
