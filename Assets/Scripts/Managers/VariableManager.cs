#pragma warning disable IDE1006 // Naming Styles
using System.Collections.Generic;
using UnityEngine;
using static SaveManager.VariableToSave;

public sealed class VariableManager : MonoBehaviour, IManager
{
    //Game variables
    [field: SerializeField, Header("Caught Timer")] public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [field: SerializeField, Header("Player Health")] public Health playerHealth { get; private set; } = new(100);

    #region Saveable variables
    //Todo: Deserialise these later.
    [field: Header("Player Resources")]
    [field: SerializeField] public int Bytecoins { get; private set; } = 0;
    [field: SerializeField] public int IntelligenceData { get; private set; } = 0;
    [field: SerializeField] public int ProcessingPower { get; private set; } = 0;

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
    #endregion

    public void StartUp()
    {
        Restart();
        //TODO: Attempt to load file otherwise load default stuff here.
        SaveManager sav = GameManager.SaveManager;
        Bytecoins = sav.LoadVariable<int>(bytecoins);
        IntelligenceData = sav.LoadVariable<int>(intelligenceData);
        ProcessingPower = sav.LoadVariable<int>(processingPower);
        allUnlocks = sav.LoadVariable<Dictionary<AllUnlockables, Unlockable>>(allUnlockables);
        if (allUnlocks != null)
        {
            return;
        }

        allUnlocks = new()
        {
            { AllUnlockables.Dash, new Upgradeable(10) },
            { AllUnlockables.TrojanHorse, new Upgradeable(10) },
            { AllUnlockables.EMP, new Upgradeable(10) },
            { AllUnlockables.PhishingMinigame, new() }
        };
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
        Bytecoins -= bytecoinsAmount;
        IntelligenceData -= intelligenceDataAmount;
        ProcessingPower -= processingPowerAmount;
        SaveVariables();
    }

    public void SaveVariables()
    {
        SaveManager saveManager = GameManager.SaveManager;
        saveManager.SaveVariable(bytecoins, Bytecoins);
        saveManager.SaveVariable(intelligenceData, IntelligenceData);
        saveManager.SaveVariable(processingPower, ProcessingPower);
        saveManager.SaveVariable(allUnlockables, allUnlocks);
        saveManager.SaveToFile();
    }
}
