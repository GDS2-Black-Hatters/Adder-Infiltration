using System;

[Serializable]
public class Upgradeable : Unlockable
{
    public int Level { get; protected set; } = 0;
    public int MaxLevel { get; protected set; } = 10;

    public Upgradeable(int maxLevel)
    {
        MaxLevel = maxLevel;
    }

    public override bool IsUnlocked => Level == MaxLevel;
    public float UnlockProgression => (float)Level / MaxLevel;
    public override void Unlock() { Level++; }
    public void SetLevel(int amount) { Level = amount; }
}
