using UnityEngine;

public class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [field: SerializeField] public bool gotCaught { get; private set; } = false;
    [SerializeField] private TimeTracker timeToLive; //The timer for when getting caught.
    [SerializeField] private GameObject timer;

    //Saveable variables

    public void StartUp()
    {
    }

    /// <summary>
    /// When the player has been caught, call this method.
    /// </summary>
    public void StartTimer()
    {
        gotCaught = true;
    }

    /// <summary>
    /// When the level has (re)started, call this level.
    /// </summary>
    public void Restart()
    {
        timeToLive.Reset();
    }
}
