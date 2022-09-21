using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeable", menuName = "ScriptableObject/Unlockable/Upgradeable")]
public class Upgradeable : Unlockable
{
    [field: NonSerialized] public int Level { get; protected set; } = 0;
    [field: SerializeField, Min(1)] public int MaxLevel { get; protected set; } = 10;

    public override bool IsUnlocked => Level == MaxLevel;
    public float UnlockProgression => (float)Level / MaxLevel;
    public override void Unlock() { Level++; }
    public void SetLevel(int amount) { Level = amount; }
}
