using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnlockable", menuName = "ScriptableObject/Unlockable/One Time Unlock")]
public class Unlockable : ScriptableObject
{
    [NonSerialized] protected bool isUnlocked = false;
    public virtual bool IsUnlocked => isUnlocked;

    public virtual void Unlock()
    {
        isUnlocked = true;
    }
}
