using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDetector : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnDetectionEvent;

    protected void PlayerDetected()
    {
        OnDetectionEvent.Invoke();
    }
}
