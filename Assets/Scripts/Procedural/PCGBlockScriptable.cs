using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCGBlockScriptable : ScriptableObject
{
    public Vector2Int blockSize = Vector2Int.one;

    public abstract GameObject Generate(Transform rootParent = null);
}
