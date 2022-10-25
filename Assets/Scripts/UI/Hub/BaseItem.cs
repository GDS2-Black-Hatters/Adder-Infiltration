using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public abstract string Label { get; protected set; }
    public abstract Sprite Icon { get; protected set; }
}
