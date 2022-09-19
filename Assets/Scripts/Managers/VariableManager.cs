#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public sealed class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [field: SerializeField, Header("Caught Timer")] public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [field: SerializeField, Header("Player Health")] public Health playerHealth { get; private set; } = new(100);

    //Saveable variables
    [field: Header("Player Resources")]
    [field: SerializeField] public int byteCoins { get; private set; } = 0;
    [field: SerializeField] public int intelligenceData { get; private set; } = 0;
    [field: SerializeField] public int processingPower { get; private set; } = 0;

    public void StartUp()
    {
        Restart();
    }

    /// <summary>
    /// When the level has changed, call this level.
    /// </summary>
    public void Restart()
    {
        timeToLive.Reset(false, false);
        playerHealth.Reset();
    }
}
