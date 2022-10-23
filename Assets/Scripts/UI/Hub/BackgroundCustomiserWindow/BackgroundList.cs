using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBackgroundList", menuName = "ScriptableObject/Desktop/Background List")]
public class BackgroundList : ScriptableObject
{
    [field: SerializeField] public Background[] Backgrounds { get; private set; }
    [NonSerialized] private Dictionary<VariableManager.AllUnlockables, Background> backgroundDict;
    public Dictionary<VariableManager.AllUnlockables, Background> BackgroundDictionary
    {
        get
        {
            if (backgroundDict == null)
            {
                backgroundDict = new();
                foreach (Background background in Backgrounds)
                {
                    backgroundDict.Add(background.Unlockable, background);
                }
            }
            return backgroundDict;
        }
    }
}
//I know, it's the exact same as MouseList, I do not have the time to refactor with generics
//that would require reworking all the abilities.