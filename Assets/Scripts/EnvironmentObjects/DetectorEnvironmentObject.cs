using UnityEngine;

public abstract class DetectorEnvironmentObject : BaseEnvironmentObject
{
    [SerializeField] private float IncreaseAlertnessAmount = 0.2f;
    [SerializeField] protected UnityEngine.Events.UnityEvent OnDetect;

    protected virtual void Start()
    {
        if(GameManager.LevelManager.ActiveSceneController == null)
        {
            Debug.LogWarning("Cannot find SceneController, SceneController will not be alerted on player detection.");
            return;
        }            
        OnDetect.AddListener(PlayerDetectedBehaviour);
    }

    private void PlayerDetectedBehaviour()
    {
        GameManager.LevelManager.ActiveSceneController.enemyAdmin.IncreaseAlertness(IncreaseAlertnessAmount);
    }

    public abstract void PlayerInRange();
}
