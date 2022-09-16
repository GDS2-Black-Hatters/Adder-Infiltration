#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    [SerializeField, Range(0,1)] protected float mandatoryChance = 0;
    public bool isMandatory { get; protected set; }
    [field: SerializeField] public bool isComplete { get; protected set; } = false;
    public abstract string objectiveTitle { get; }

    protected virtual void Awake()
    {
        isMandatory = DoStatic.RandomBool(mandatoryChance);
    }

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
