using UnityEngine;
using UnityEngine.Events;

public class ToggleApplicationButton : BaseDesktopButton
{
    [SerializeField] private DesktopWindowApplication windowApplication;
    [SerializeField] private bool autoSetPosition = true; //Set the starting position for the window to lerp in from.
    [SerializeField] private UnityEvent clickEvent;

    private void Start()
    {
        if (autoSetPosition)
        {
            windowApplication.SetStartPos(transform.position);
        }
    }

    protected override void OnClick()
    {
        windowApplication.ToggleApplication(clickEvent);
    }
}
