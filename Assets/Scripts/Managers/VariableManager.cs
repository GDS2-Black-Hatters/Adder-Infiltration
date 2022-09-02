#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [field: SerializeField] public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [SerializeField] private GameObject caughtHUD;

    //Saveable variables

    public void StartUp()
    {
        timeToLive.Reset();
        timeToLive.onFinish += GameOver;
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

    private void GameOver() //Might not be the right to put this...
    {
        Debug.Log("Game Over code here.");
    }
}
