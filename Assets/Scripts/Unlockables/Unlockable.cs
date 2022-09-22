using System;

[Serializable]
public class Unlockable
{
    protected bool isUnlocked = false;
    public virtual bool IsUnlocked => isUnlocked;

    public virtual void Unlock()
    {
        isUnlocked = true;
    }
}
