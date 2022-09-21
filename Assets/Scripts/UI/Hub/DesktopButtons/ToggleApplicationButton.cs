using UnityEngine;

public class ToggleApplicationButton : BaseDesktopButton
{
    [SerializeField] private DesktopWindowApplication windowApplication;
    [SerializeField] private bool autoSetPosition = false;

    private void Start()
    {
        if (autoSetPosition)
        {
            windowApplication.SetStartPos(transform.position);
        }
    }

    protected override void OnClick()
    {
        windowApplication.ToggleApplication();
    }
}
