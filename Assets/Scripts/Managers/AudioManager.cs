using UnityEngine;

public sealed class AudioManager : BaseManager
{
    [SerializeField] private AK.Wwise.RTPC volumeRTPC;

    public override BaseManager StartUp()
    {
        volumeRTPC.SetGlobalValue(GameManager.VariableManager.GetVariable<float>(SaveManager.VariableToSave.audioVolume));
        return this;
    }
}
