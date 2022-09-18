#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    [SerializeField, Range(0,1)] protected float mandatoryChance = 0;
    public bool isMandatory { get; protected set; }
    [field: SerializeField] public bool isComplete { get; protected set; } = false;
    public abstract string objectiveTitle { get; }
    public bool canBeMandatory { get; protected set; } = true;

    protected virtual void Awake()
    {
        isMandatory = DoStatic.RandomBool(mandatoryChance);
        GameManager.LevelManager.ActiveSceneController.AddToObjectiveList(this);
    }

    public void ForceMandatory()
    {
        isMandatory = true;
    }

    protected virtual void OnDestroy()
    {
        isComplete = true;
        GameManager.LevelManager.ActiveSceneController.UpdateObjectiveList();
    }
}
