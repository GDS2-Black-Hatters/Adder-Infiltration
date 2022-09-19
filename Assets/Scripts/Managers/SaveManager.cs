using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public sealed class SaveManager : MonoBehaviour, IManager
{
    /// <summary>
    /// These variables are saved to file.
    /// <br></br>
    /// <br></br>RULES:
    /// <br></br>   1. EVERY ENUM MUST BE ASSIGNED TO A UNIQUE NUMBER
    /// <br></br>   2. Name appropriately.
    /// <br></br>
    /// <br></br>If the need to change an enum's unique number to another number
    /// you must delete all save files as they will become "corrupt".
    /// </summary>
    public enum VariableToSave
    {
        //Player resources
        bytecoins = 1000,
        intelligenceData = 1001,
        processingPower = 1002,
    }

    private string saveFile;
    private readonly BinaryFormatter formatter = new(); //Converts data from and into a serialised format.
    private Dictionary<string, object> savedVars = new(); //The dictionary of where all the data is saved.

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
    /// <param name="key">The string of the variable name, will error if there is more than one key.</param>
    /// <param name="obj">Any variable that can be serialised.</param>
    public void SaveVariable(VariableToSave variable, object obj)
    {
        string key = DoStatic.EnumAsString(variable);
        if (savedVars.ContainsKey(key))
        {
            savedVars[key] = obj;
        }
        else
        {
            savedVars.Add(key, obj);
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
        savedVars = (Dictionary<string, object>)formatter.Deserialize(file);
        file.Close();
    }

    //These classes is to just help differentiate the LoadVariable method overload.
    public class RequireStruct<T> where T : struct { }
    public class RequireClass<T> where T : class { }

    /// <summary>
    /// Grab a variable from the save file.
    /// </summary>
    /// <param name="key">The key for the wanted variable.</param>
    /// <returns>The object if the key exists otherwise returns null.</returns>
    public T LoadVariable<T>(VariableToSave variable, RequireClass<T> _ = null) where T : class
    {
        string key = DoStatic.EnumAsString(variable);
        return savedVars.ContainsKey(key) ? (T)savedVars[key] : null;
    }

    /// <summary>
    /// Grab a variable from the save file.
    /// </summary>
    /// <param name="key">The key for the wanted variable.</param>
    /// <returns>The object if the key exists otherwise returns default.</returns>
    public T LoadVariable<T>(VariableToSave variable, RequireStruct<T> _ = null) where T : struct
    {
        string key = DoStatic.EnumAsString(variable);
        return savedVars.ContainsKey(key) ? (T)savedVars[key] : default;
    }
    #endregion
}
