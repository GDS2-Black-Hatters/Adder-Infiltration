#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [field: SerializeField, Header("Caught Timer")] public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [field: SerializeField, Header("Player Health")] public Health playerHealth { get; private set; } = new(100);

    //Saveable variables

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
