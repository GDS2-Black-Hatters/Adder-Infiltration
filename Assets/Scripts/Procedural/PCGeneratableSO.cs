using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PCGeneratableSO : ScriptableObject
{
    public abstract IEnumerator Generate(Transform parentTransform, GameObject rootGameObject, MonoBehaviour generator, Action incompleteCall);
}
