using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaggerDebug : MonoBehaviour
{
    [SerializeField] private int defaultFrameRate = -1;
    [SerializeField] private int lagFrameRate = 5;

    [SerializeField] private bool isLagging = false;

    private void Update()
    {
        LagToggle(isLagging);
    }

    public void ToggleLagInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        isLagging = !isLagging;
        Debug.Log("Lag Toggled.");
        LagToggle(isLagging);
    }

    public void LagToggle(bool EnableLag)
    {
        if(EnableLag)
        {
            Application.targetFrameRate = lagFrameRate;
        }
        else
        {
            Application.targetFrameRate = defaultFrameRate;
        }
    }
}
