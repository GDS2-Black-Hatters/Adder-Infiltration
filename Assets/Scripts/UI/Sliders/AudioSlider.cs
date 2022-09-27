using UnityEngine;

public class AudioSlider : BaseSlider
{
    [SerializeField] private AK.Wwise.RTPC volumeRTPC;

    protected override void Awake()
    {
        base.Awake();
        slider.value = 50; //TODO: Grab the value from the save file.
    }

    protected override void OnValueChanged(float value)
    {
        volumeRTPC.SetGlobalValue(value);
    }
}
