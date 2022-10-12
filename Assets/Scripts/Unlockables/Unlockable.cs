using System;

[Serializable]
public class Unlockable
{
    public bool IsUnlocked { get; private set; } = false;

    public virtual void Unlock()
    {
        IsUnlocked = true;
    }
}
