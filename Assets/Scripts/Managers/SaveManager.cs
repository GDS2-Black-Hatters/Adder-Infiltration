using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour, IManager
{
    private string saveFilePath;
    private readonly BinaryFormatter formatter = new(); //Converts data from and into a serialised format.
    private Dictionary<string, object> savedVars = new(); //The dictionary of where all the data is saved.

    public void StartUp()
    {
        saveFilePath = Application.persistentDataPath + "/";
        LoadFile(saveFilePath + "AdderInfiltration.sav");
    }

    /// <summary>
    /// Scans for save files.
    /// </summary>
    /// <returns>Returns a array of save file names</returns>
    public string[] ScanForSaveFiles()
    {
        List<string> saveFiles = new();
        foreach (string file in Directory.GetFiles(saveFilePath))
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
        FileStream file = File.Create(saveFilePath); //Overwrites the old file.
        formatter.Serialize(file, savedVars);
        file.Close();
    }

    /// <summary>
    /// Saves the variable called through from other components.
    /// Warning: If the key already exists, the value of it will be overwritten.
    /// </summary>
    /// <param name="key">The string of the variable name, will error if there is more than one key.</param>
    /// <param name="obj">Any variable that can be serialised.</param>
    public void SaveVariable(string key, object obj)
    {
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
    public T LoadVariable<T>(string key, RequireClass<T> _ = null) where T : class
    {
        return savedVars.ContainsKey(key) ? (T)savedVars[key] : null;
    }

    /// <summary>
    /// Grab a variable from the save file.
    /// </summary>
    /// <param name="key">The key for the wanted variable.</param>
    /// <returns>The object if the key exists otherwise returns default.</returns>
    public T LoadVariable<T>(string key, RequireStruct<T> _ = null) where T : struct
    {
        return savedVars.ContainsKey(key) ? (T)savedVars[key] : default;
    }
    #endregion
}
