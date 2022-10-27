#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    [field: SerializeField] public bool isComplete { get; protected set; } = false;
    [field: SerializeField] public string objectiveTitle { get; protected set; }

    [SerializeField] protected AK.Wwise.Event ObjectiveCompleteSound;
    [SerializeField] protected UnityEngine.Events.UnityEvent OnObjectiveComplete;

    protected virtual void Start()
    {
        GameManager.LevelManager.ActiveSceneController.AddToObjectiveList(this);
    }

    protected virtual void ObjectiveFinish()
    {
        enabled = false;
        ObjectiveCompleteSound.Post(gameObject);
        isComplete = true;
        OnObjectiveComplete.Invoke();
    }
}
