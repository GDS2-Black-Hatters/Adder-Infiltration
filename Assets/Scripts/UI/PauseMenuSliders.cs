using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuSliders : MonoBehaviour
{
    
    public InputActionAsset inputActionAsset; //In the Editor Property panel, drag / drop the new input system's action map from the project assets to this slot
    public InputAction inputActionLook;
    public InputActionReference lookAction;

    public AK.Wwise.RTPC volumeRTPC;
    public void adjustMouseSensitivity(float newSensitivity)
    {
        lookAction.action.ApplyParameterOverride("scaleVector2:x", newSensitivity);
        lookAction.action.ApplyParameterOverride("scaleVector2:y", newSensitivity/100);
    }

    public void adjustAudioVolume(float newVolume)
    {
        volumeRTPC.SetGlobalValue(newVolume);
    }
}
