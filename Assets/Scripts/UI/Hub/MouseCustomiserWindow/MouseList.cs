using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMouseList", menuName = "ScriptableObject/Mouse/Mouse List")]
public class MouseList : ScriptableObject
{
    [field: SerializeField] public Mouse[] Mouses { get; private set; }
    [NonSerialized] private Dictionary<VariableManager.AllUnlockables, Mouse> mouseDict;
    public Dictionary<VariableManager.AllUnlockables, Mouse> MouseDictionary
    {
        get
        {
            if (mouseDict == null)
            {
                mouseDict = new();
                foreach(Mouse mouse in Mouses)
                {
                    mouseDict.Add(mouse.Unlockable, mouse);
                }
            }
            return mouseDict;
        } 
    }
}
