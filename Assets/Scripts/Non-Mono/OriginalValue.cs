#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

/// <summary>
/// This class is perfect for values that change regularly but also needs to be reset often.
/// Example: HP, MP
/// </summary>
/// <typeparam name="T">Literally any datatype</typeparam>
[System.Serializable]
public class OriginalValue<T>
{
    [field: SerializeField] public T originalValue { get; private set; }
    [HideInInspector] public T value;

    public OriginalValue(T originalValue)
    {
        SetOriginalValue(originalValue);
    }

    public void SetOriginalValue(T newValue, bool autoReset = true)
    {
        originalValue = newValue;
        if (autoReset)
        {
            Reset();
        }
    }

    public void Reset()
    {
        value = originalValue;
    }
}