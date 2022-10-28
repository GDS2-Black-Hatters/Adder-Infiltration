using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public sealed class SaveManager : BaseManager
{
    /// <summary>
    /// These variables are saved to file.
    /// <br/>
    /// <br/> Rules:
    /// <br/> 1. Every enum must be assigned to a unique number.
    /// <br/> 2. You can add more enums but never remove them.
    /// <br/> 3. Name appropriately. (Feel free to rename enums)
    /// <br/>
    /// <br/> If the need to change an enum's unique number to another
    /// <br/> number or remove an enum you must delete all save files as
    /// <br/> they will become "corrupt" and all references in them
    /// <br/> in the project will be affected.
    /// </summary>
    public enum VariableToSave
    {
        //Player resources
        bytecoins = 1000,
        intelligenceData = 1001,
        processingPower = 1002,
        allUnlockables = 1003,
        minigameHighScores = 1004,
        tutorialFinish = 1005,

        mouseSprite = 2000,
        mouseSensitivity = 2001,
        audioVolume = 2002,
        currentDesktopBackground = 2003,
    }

    private string saveFile;
    private readonly BinaryFormatter formatter = new(); //Converts data from and into a serialised format.
    [SerializeField] private GameObject saveIcon;

    public override BaseManager StartUp()
    {
        saveFile = Application.persistentDataPath + "/AdderInfiltrationSprint5.sav";
        print(Application.persistentDataPath); //Uncomment to find where it is stored.
        LoadFile(saveFile);
        return this;
    }

    /// <summary>
    /// Saves the current state of the dictionary.
    /// </summary>
    public void SaveToFile(bool showFeedback = true)
    {
        saveIcon.SetActive(showFeedback);
        FileStream file = File.Create(saveFile); //Overwrites the old file.
        formatter.Serialize(file, GameManager.VariableManager.GetAllVars());
        file.Close();
    }

    private void LoadFile(string filename)
    {
        //Attempt to find it. Return a new
        if (!File.Exists(filename))
        {
            GameManager.VariableManager.SetAllVars(new());
            return;
        }

        FileStream file = File.Open(filename, FileMode.Open);
        GameManager.VariableManager.SetAllVars((Dictionary<VariableToSave, object>)formatter.Deserialize(file));
        file.Close();
    }
}
