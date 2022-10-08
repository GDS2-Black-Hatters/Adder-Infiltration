#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;
using static SaveManager.VariableToSave;

public sealed class VariableManager : BaseManager
{
    //Game variables
    [SerializeField] private GameObject abilities;
    private readonly Dictionary<string, AbilityBase> allAbilities = new();

    [field: SerializeField, Header("Caught Timer")] public TimeTracker timeToLive { get; private set; } //The timer for when getting caught. Is in seconds.
    [field: SerializeField, Header("Player Health")] public Health playerHealth { get; private set; } = new(100);

    private Dictionary<VariableToSave, object> savedVars; //The dictionary of where all the data is saved.
    public event Action purchaseCallback;
    /// <summary>
    /// Read and follow all rules in description of SaveManager.VariableToSave
    /// </summary>
    public enum AllUnlockables
    {
        //Upgradeables
        Dash = 10,
        TrojanHorse = 11,
        EMP = 12,
        Warp = 13,

        //One time unlocks
        PhishingMinigame = 101
    }

    public enum AllAbilities
    {
        Dash = 0,
        TrojanHorse = 1,
        EMP = 2,
        Warp = 3,
    }

    public override BaseManager StartUp()
    {
        Restart();

        foreach (AbilityBase ability in abilities.GetComponentsInChildren<AbilityBase>())
        {
            allAbilities.Add(ability.name, ability);
        }

        return this;
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
        return GetVariable<Dictionary<AllUnlockables, Unlockable>>(allUnlockables)[abilityName];
    }

    public AbilityBase GetAbility(AllAbilities abilities)
    {
        return allAbilities[DoStatic.EnumToString(abilities)];
    }

    public void Purchase(int bytecoinsAmount, int intelligenceDataAmount, int processingPowerAmount)
    {
        SetVariable(bytecoins, GetVariable<int>(bytecoins) - bytecoinsAmount);
        SetVariable(intelligenceData, GetVariable<int>(intelligenceData) - intelligenceDataAmount);
        SetVariable(processingPower, GetVariable<int>(processingPower) - processingPowerAmount);
        purchaseCallback?.Invoke();
        GameManager.SaveManager.SaveToFile();
    }

    public void RandomIncrement()
    {
        SetVariable(bytecoins, GetVariable<int>(bytecoins) + UnityEngine.Random.Range(0, 100));
        SetVariable(intelligenceData, GetVariable<int>(intelligenceData) + UnityEngine.Random.Range(0, 100));
        SetVariable(processingPower, GetVariable<int>(processingPower) + UnityEngine.Random.Range(0, 100));
        GameManager.SaveManager.SaveToFile();
    }

    #region Use carefully.
    /// <summary>
    /// This should not be used.
    /// Replace all saved variables with a new dictionary.
    /// It is recommended to use SetVariable() as it is much safer.
    /// </summary>
    /// <param name="vars">A dictionary of all the saved variables.</param>
    public void SetAllVars(Dictionary<VariableToSave, object> vars)
    {
        savedVars = vars;
        AddMissingDefaults();
    }

    private void AddMissingDefaults()
    {
        Dictionary<VariableToSave, object> defaultValues = new()
        {
            {bytecoins, 0 },
            {intelligenceData, 0 },
            {processingPower, 0 },
            { allUnlockables, new Dictionary<AllUnlockables, Unlockable>()
                {
                    { AllUnlockables.Dash, new Upgradeable(10) },
                    { AllUnlockables.TrojanHorse, new Upgradeable(10) },
                    { AllUnlockables.EMP, new Upgradeable(10) },
                    { AllUnlockables.Warp, new Upgradeable(10) },
                    { AllUnlockables.PhishingMinigame, new() },
                }
            },
            {mouseSensitivity, 0.09f },
            {audioVolume, 75f }
        };

        #region Ensure Default Integrity
        if (DoStatic.EnumList<VariableToSave>().Length != defaultValues.Count)
        {
            Debug.LogError("WARNING, DEFAULT VALUES ARE MISSING.");
        }

        if (DoStatic.EnumList<AllUnlockables>().Length != ((Dictionary<AllUnlockables, Unlockable>)defaultValues[allUnlockables]).Count)
        {
            Debug.LogError("WARNING, UNLOCKABLE DEFAULT VALUES ARE MISSING.");
        }
        #endregion

        foreach (VariableToSave key in defaultValues.Keys)
        {
            if (!savedVars.ContainsKey(key))
            {
                SetVariable(key, defaultValues[key]);
            }
        }

        Dictionary<AllUnlockables, Unlockable> defaultDict = (Dictionary<AllUnlockables, Unlockable>)defaultValues[allUnlockables];
        foreach (AllUnlockables unlockable in defaultDict.Keys)
        {
            Dictionary<AllUnlockables, Unlockable> dict = (Dictionary<AllUnlockables, Unlockable>)savedVars[allUnlockables];
            if (!dict.ContainsKey(unlockable))
            {
                dict.Add(unlockable, defaultDict[unlockable]);
            }
        }
    }

    /// <summary>
    /// Get all the variables.
    /// It is recommended to use GetVariable<T>() as it is much safer.
    /// </summary>
    /// <returns>A dictionary of all the variables.</returns>
    public Dictionary<VariableToSave, object> GetAllVars()
    {
        return savedVars;
    }
    #endregion

    #region Getters/Setters for savedVars
    /// <summary>
    /// Saves the variable called through from other components.
    /// Warning: If the key already exists, the value of it will be overwritten.
    /// </summary>
    /// <param name="variable">The variable name.</param>
    /// <param name="obj">Any variable that can be serialised.</param>
    public void SetVariable(VariableToSave variable, object obj)
    {
        savedVars[variable] = obj;
    }

    /// <summary>
    /// Grab a variable from the save file.
    /// <br/>
    /// <br/>NOTE: Since cast is from object to datatype, the datatype must always be exact.
    /// <br/>For example if the saved variable is an integer and the casting is from object
    /// <br/>to float, a cast exception will occur due not casting it from object to integer first.
    /// </summary>
    /// <param name="variable">The the wanted variable.</param>
    /// <typeparam name="T">Any variable type. Always specify the correct datatype.</typeparam>
    /// <returns>The key value.</returns>
    public T GetVariable<T>(VariableToSave variable)
    {
        return (T)savedVars[variable];
    }
    #endregion
}
