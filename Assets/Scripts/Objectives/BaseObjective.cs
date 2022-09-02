#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    [field: SerializeField] public bool isMandatory { get; protected set; } = false;
    [field: SerializeField] public bool isComplete { get; protected set; } = false;
    public string objectiveTitle { get; protected set; }

    protected virtual void Start()
    {
        GameManager.LevelManager.ActiveSceneController.AddToObjectiveList(this);
    }

    protected virtual void OnDestroy()
    {
        isComplete = true;
        GameManager.LevelManager.ActiveSceneController.UpdateObjectiveList();
    }
}
