using UnityEngine;

public abstract class DetectorEnvironmentObject : MonoBehaviour
{
    [SerializeField] private bool AlertLevelSceneManager = true;
    [SerializeField] protected UnityEngine.Events.UnityEvent OnDetect;

    protected virtual void Start()
    {
        if(AlertLevelSceneManager)
        {
            if(GameManager.LevelManager.ActiveSceneController == null)
            {
                Debug.LogWarning("Cannot find SceneController, SceneController will not be alerted on player detection.");
                return;
            }            
            OnDetect.AddListener(GameManager.LevelManager.ActiveSceneController.StartCaughtMode);
        }
    }

    public abstract void PlayerInRange();
}
