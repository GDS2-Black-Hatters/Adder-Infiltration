using UnityEngine;
using System;

[Serializable]
public class Unlockable
{
    [field: SerializeField, Header("Unlockable")] public bool IsUnlocked { get; private set; } = false;

    public Unlockable(bool isUnlocked = false)
    {
        IsUnlocked = isUnlocked;
    }

    public virtual void Unlock()
    {
        IsUnlocked = true;
    }
}
