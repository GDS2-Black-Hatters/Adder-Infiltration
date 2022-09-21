using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public sealed class SaveManager : MonoBehaviour, IManager
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
    /// <br/> they will become "corrupt".
    /// </summary>
    public enum VariableToSave
    {
        //Player resources
        bytecoins = 1000,
        intelligenceData = 1001,
        processingPower = 1002,
        allUnlockables = 1003,
    }

    private string saveFile;
    private readonly BinaryFormatter formatter = new(); //Converts data from and into a serialised format.
    private Dictionary<VariableToSave, object> savedVars = new(); //The dictionary of where all the data is saved.

    public void StartUp()
    {
        saveFile = Application.persistentDataPath + "/AdderInfiltration.sav"; //Debug log the variable to find where it is stored.
        LoadFile(saveFile);
    }

    /// <summary>
    /// Scans for save files.
    /// </summary>
    /// <returns>Returns a array of save file names</returns>
    public string[] ScanForSaveFiles()
    {
        List<string> saveFiles = new();
        foreach (string file in Directory.GetFiles(saveFile))
        {
            if (file.EndsWith(".sav"))
            {
                saveFiles.Add(file.Replace(".sav", ""));
            }
        }
        return saveFiles.ToArray();
    }

    #region Save Methods
    /// <summary>
    /// Saves the current state of the dictionary.
    /// </summary>
    public void SaveToFile()
    {
        FileStream file = File.Create(saveFile); //Overwrites the old file.
        formatter.Serialize(file, savedVars);
        file.Close();
    }

    /// <summary>
    /// Saves the variable called through from other components.
    /// Warning: If the key already exists, the value of it will be overwritten.
    /// </summary>
    /// <param name="variable">The variable name.</param>
    /// <param name="obj">Any variable that can be serialised.</param>
    public void SaveVariable(VariableToSave variable, object obj)
    {
        try
        {
            savedVars[variable] = obj;
        }
        catch
        {
            savedVars.Add(variable, obj);
        }
    }
    #endregion

    #region Load Methods
    private void LoadFile(string filename)
    {
        //Attempt to find it. Return a new
        if (!File.Exists(filename))
        {
            return;
        }

        savedVars.Clear();
        FileStream file = File.Open(filename, FileMode.Open);
        savedVars = (Dictionary<VariableToSave, object>)formatter.Deserialize(file);
        file.Close();
    }

    //These classes is to just help differentiate the LoadVariable method overload.
    public class RequireStruct<T> where T : struct { }
    public class RequireClass<T> where T : class { }

    /// <summary>
    /// Grab a variable from the save file.
    /// </summary>
    /// <param name="variable">The wanted variable.</param>
    /// <returns>The object if the key exists otherwise returns null.</returns>
    public T LoadVariable<T>(VariableToSave variable, RequireClass<T> _ = null) where T : class
    {
        return savedVars.ContainsKey(variable) ? (T)savedVars[variable] : null;
    }

    /// <summary>
    /// Grab a variable from the save file.
    /// </summary>
    /// <param name="variable">The the wanted variable.</param>
    /// <returns>The object if the key exists otherwise returns default.</returns>
    public T LoadVariable<T>(VariableToSave variable, RequireStruct<T> _ = null) where T : struct
    {
        return savedVars.ContainsKey(variable) ? (T)savedVars[variable] : default;
    }
    #endregion
}
