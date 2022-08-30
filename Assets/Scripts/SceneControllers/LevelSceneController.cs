using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneController : BaseSceneController
{
    [SerializeField] public UnityEngine.Events.UnityEvent OnPlayerDetected;

    private void Awake()
    {
        GameManager.LevelManager.SetActiveSceneController(this);
    }

    public void PlayerDetected()
    {
        Debug.Log("SceneController Alerted on Player Detection");
        OnPlayerDetected?.Invoke();
    }
}
