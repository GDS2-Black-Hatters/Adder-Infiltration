using System;
using UnityEngine;

[Serializable]
public class Upgradeable : Unlockable
{
    public int Level { get; protected set; } = 0;
    [field: SerializeField, Header("Upgradeable")] public int MaxLevel { get; protected set; } = 10;

    public Upgradeable(int maxLevel)
    {
        MaxLevel = maxLevel;
    }

    public float UnlockProgression => (float)Level / MaxLevel;
    
    public override void Unlock()
    {
        if (IsUnlocked)
        {
            Level++;
        }
        base.Unlock();
    }

    public void SetLevel(int amount) { Level = amount; }
}
