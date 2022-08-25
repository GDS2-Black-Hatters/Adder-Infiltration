using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDetector : MonoBehaviour
{
    public event System.Action OnDetection;

    protected void PlayerDetected()
    {
        OnDetection?.Invoke();
    }
}
