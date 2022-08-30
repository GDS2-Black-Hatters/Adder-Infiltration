#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [Header("Caught Timer")]
    [SerializeField] private float countdownTimer;
    public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [SerializeField] private GameObject caughtHUD;


    [Header("Player Health")]
    [SerializeField] private float maxPlayerHealth = 100;
    public Health playerHealth { get; private set; }

    //Saveable variables

    public void StartUp()
    {
        timeToLive = new(countdownTimer);
        playerHealth = new(maxPlayerHealth);
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
        playerHealth.Reset();
    }
}
