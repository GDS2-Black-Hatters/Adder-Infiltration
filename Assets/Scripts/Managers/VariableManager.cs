#pragma warning disable IDE1006 // Naming Styles
using System.Collections.Generic;
using UnityEngine;

public sealed class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [field: SerializeField, Header("Caught Timer")] public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [field: SerializeField, Header("Player Health")] public Health playerHealth { get; private set; } = new(100);

    #region Saveable variables
    //Todo: Deserialise these later.
    [field: Header("Player Resources")]
    [field: SerializeField] public int bytecoins { get; private set; } = 0;
    [field: SerializeField] public int intelligenceData { get; private set; } = 0;
    [field: SerializeField] public int processingPower { get; private set; } = 0;

    /// <summary>
    /// Similar description to SaveManager.VariableToSave
    /// </summary>
    public enum AllUnlockables
    {
        //Upgradeables
        Dash = 10,
        TrojanHorse = 11,
        EMP = 12,

        //One time unlocks
        PhishingMinigame = 101
    }
    private Dictionary<AllUnlockables, Unlockable> allUnlocks;
    [SerializeField, Header("All Unlockables")] private FakeDictionary<AllUnlockables, Unlockable> unlocks;
    #endregion

    public void StartUp()
    {
        Restart();
        allUnlocks = unlocks.ToDictionary();
    }

    /// <summary>
    /// When the level has changed, call this level.
    /// </summary>
    public void Restart()
    {
        timeToLive.Reset(false, false);
        playerHealth.Reset();
    }

    public Unlockable GetUnlockable(AllUnlockables abilityName)
    {
        return allUnlocks[abilityName];
    }

    public void Purchase(int bytecoinsAmount, int intelligenceDataAmount, int processingPowerAmount)
    {
        bytecoins -= bytecoinsAmount;
        intelligenceData -= intelligenceDataAmount;
        processingPower -= processingPowerAmount;
    }
}
