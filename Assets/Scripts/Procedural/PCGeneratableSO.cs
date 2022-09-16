using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCGeneratableSO : ScriptableObject
{
    public abstract GameObject Generate(Transform parentTransform, float cellUnitMultiplier);
}
