#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    public TimeTracker timeToLive { get; private set; } //The timer for when getting caught.
    [SerializeField] private GameObject caughtHUD;

    //Saveable variables

    public void StartUp()
    {
    }

    /// <summary>
    /// When the player has been caught, call this method.
    /// </summary>
    public void StartTimer()
    {
        caughtHUD.SetActive(true);
    }

    /// <summary>
    /// When the level has (re)started, call this level.
    /// </summary>
    public void Restart()
    {
        timeToLive.Reset();
    }
}
