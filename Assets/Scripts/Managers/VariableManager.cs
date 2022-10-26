#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;
using static SaveManager.VariableToSave;
using static VariableManager.AllUnlockables;

public sealed class VariableManager : BaseManager
{
    //Game variables
    [field: SerializeField] public AbilityList allAbilities { get; private set; }
    [field: SerializeField] public MouseList MouseList { get; private set; }
    [field: SerializeField] public BackgroundList BackgroundList { get; private set; }
    private readonly Dictionary<AllAbilities, Ability> abilityDictionary = new();

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
        Scan = 13,
        Protect = 14,
        Heal = 15,

        //One time unlocks
        PhishingMinigame = 101,
        RunMinigame = 102,

        DefaultMouse = 200,
        SnakeMouse = 201,
        MouseMouse = 202,
        USBMouse = 203,
        PirateMouse = 204,
        SpiderMouse = 205,
        BinaryMouse = 206,
        PhishyMouse = 207,

        DefaultBackground = 300,
        PaleCity = 301,
        LittleRedTitle = 302,
        LittleRedCottage = 303,
        MoonParadise = 304,
        Gaster = 305,
    }

    //Kinda regret adding this...
    public enum AllAbilities
    {
        Dash = 0,
        TrojanHorse = 1,
        EMP = 2,
        Scan = 3,
        Protect = 4,
        Heal = 5,
    }

    public override BaseManager StartUp()
    {
        foreach (Ability ability in allAbilities.Abilities)
        {
            abilityDictionary.Add(ability.AbilityID, ability);
        }

        return this;
    }

#if UNITY_EDITOR
    [SerializeField] private bool MaxMoney = false;
    private void Update()
    {
        if (MaxMoney)
        {
            MaxMoney = false;
            SetVariable(bytecoins, 9999);
            SetVariable(intelligenceData, 9999);
            SetVariable(processingPower, 9999);
            GameManager.SaveManager.SaveToFile(true);
        }
    }
#endif

    public Unlockable GetUnlockable(AllUnlockables abilityName)
    {
        return GetVariable<Dictionary<AllUnlockables, Unlockable>>(allUnlockables)[abilityName];
    }

    public Ability GetAbility(AllAbilities abilities)
    {
        return abilityDictionary[abilities];
    }

    public int GetMinigameScore(AllUnlockables minigameName)
    {
        return GetVariable<Dictionary<AllUnlockables, int>>(minigameHighScores)[minigameName];
    }

    /// <summary>
    /// Updates a minigame high score
    /// </summary>
    /// <param name="minigameName">The unlockable minigame</param>
    /// <param name="newScore">The potential new high score</param>
    /// <returns>True if it was a new high score.</returns>
    public bool UpdateMinigameHighScore(AllUnlockables minigameName, int newScore)
    {
        var highScores = GetVariable<Dictionary<AllUnlockables, int>>(minigameHighScores);
        bool showFeedback = highScores[minigameName] < newScore;
        if (showFeedback)
        {
            highScores[minigameName] = newScore;
        }
        GameManager.SaveManager.SaveToFile(showFeedback);
        InvokePurchase(); //Technically just updates the resources count.
        return showFeedback;
    }

    public void Purchase(int bytecoinsAmount, int intelligenceDataAmount, int processingPowerAmount)
    {
        SetVariable(bytecoins, GetVariable<int>(bytecoins) - bytecoinsAmount);
        SetVariable(intelligenceData, GetVariable<int>(intelligenceData) - intelligenceDataAmount);
        SetVariable(processingPower, GetVariable<int>(processingPower) - processingPowerAmount);
    }

    public void InvokePurchase()
    {
        purchaseCallback?.Invoke();
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
            { bytecoins, 100 }, //TODO: Set to 0 for next sprint.
            { intelligenceData, 100 },
            { processingPower, 100 },
            { allUnlockables, new Dictionary<AllUnlockables, Unlockable>()
                {
                    { AllUnlockables.Dash, GetAbility(AllAbilities.Dash).DefaultUpgrade },
                    { AllUnlockables.TrojanHorse, GetAbility(AllAbilities.TrojanHorse).DefaultUpgrade },
                    { AllUnlockables.EMP, GetAbility(AllAbilities.EMP).DefaultUpgrade },
                    { AllUnlockables.Scan, GetAbility(AllAbilities.Scan).DefaultUpgrade },
                    { AllUnlockables.Protect, GetAbility(AllAbilities.Protect).DefaultUpgrade },
                    { AllUnlockables.Heal, GetAbility(AllAbilities.Heal).DefaultUpgrade },

                    { DefaultMouse, new(true) },
                    { PhishingMinigame, new() },
                    { RunMinigame, new() },
                    { SnakeMouse, new() },
                    { MouseMouse, new() },
                    { USBMouse, new() },
                    { PirateMouse, new() },
                    { SpiderMouse, new() },
                    { BinaryMouse, new() },
                    { PhishyMouse, new() },

                    { DefaultBackground, new(true) },
                    { PaleCity, new() },
                    { LittleRedTitle, new() },
                    { LittleRedCottage, new() },
                    { MoonParadise, new() },
                    { Gaster, new() },
                }
            },
            { minigameHighScores, new Dictionary<AllUnlockables, int>()
                {
                    { PhishingMinigame, 0 },
                    { RunMinigame, 0 }
                }
            },
            { mouseSprite, DefaultMouse },
            { mouseSensitivity, 0.09f },
            { audioVolume, 75f },
            { currentDesktopBackground, DefaultBackground },
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
        Dictionary<AllUnlockables, Unlockable> dict = (Dictionary<AllUnlockables, Unlockable>)savedVars[allUnlockables];
        foreach (AllUnlockables unlockable in defaultDict.Keys)
        {
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
