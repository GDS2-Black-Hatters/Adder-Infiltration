using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDetector : MonoBehaviour
{
    public event System.Action OnDetection;

    protected virtual void Start()
    {
        LevelSceneController lsc = (LevelSceneController)GameManager.LevelManager.ActiveSceneController;
        if(lsc == null)
        {
            Debug.LogWarning("Cannot find LevelSceneController, will do nothing on player detection.");
            return;
        }
        OnDetection += lsc.PlayerDetected;
    }

    protected void PlayerDetected()
    {
        OnDetection?.Invoke();
    }
}
