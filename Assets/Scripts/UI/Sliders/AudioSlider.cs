using UnityEngine;

public class AudioSlider : BaseSlider
{
    [SerializeField] private AK.Wwise.RTPC volumeRTPC;

    protected override void OnValueChanged(float value)
    {
        base.OnValueChanged(value);
        volumeRTPC.SetGlobalValue(value);
    }
}
