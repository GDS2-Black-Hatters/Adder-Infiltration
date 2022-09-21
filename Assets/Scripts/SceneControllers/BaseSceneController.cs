#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneController : MonoBehaviour
{
    public enum SceneState
    {
        Stealth,
        Combat,
    }
    public SceneState sceneMode { get; private set; } = SceneState.Stealth;

    public bool canFinish { get; private set; } = false;
    [field: SerializeField] public EnemyAdmin enemyAdmin { get; private set; }
    private List<Transform> playerSpawnPoints = new();
    private bool hasAMandatoryObjective = false;

    //For children.
    protected List<BaseObjective> objectives = new();
    public event Action onPlayerDetection;

    //Skybox Lerping.
    [Header("Skybox Parameters")]
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private float lerpTime = 1f;
    private readonly Lerper lerper = new();

    //MatterShell
    private MatterShell matterShell;

    protected virtual void Awake()
    {
        GameManager.LevelManager.SetActiveSceneController(this);

        //Create and assign a copy so we don't change the asset original values
        RenderSettings.skybox = new(RenderSettings.skybox);
    }

    protected virtual void Start()
    {
        enemyAdmin.onFullAlert += StartCaughtMode;
    }

    protected virtual void Update()
    {
        UpdateObjectiveList();
        if (lerper.isLerping)
        {
            lerper.Update(Time.deltaTime);
            RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(startColor, endColor, lerper.currentValue));
        }
    }

    public void AddSpawnPoint(Transform transform)
    {
        playerSpawnPoints.Add(transform);
    }

    public void SetSpawnPoint()
    {
        GameManager.LevelManager.player.position = playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Count)].position;
    }

    public void StartCaughtMode()
    {
        if (sceneMode == SceneState.Stealth)
        {
            sceneMode = SceneState.Combat;
            onPlayerDetection?.Invoke();
            lerper.SetValues(0, 1, lerpTime);
            if (matterShell)
            {
                matterShell.WeaponizeMatter(); //Should be removed, matter shell converstion now handled by player virus state controller
            }
        }
    }

    public void SetMatterShell(MatterShell shell)
    {
        matterShell = shell;
    }

    public void AddToObjectiveList(BaseObjective objective)
    {
        objectives.Add(objective);
        if (!hasAMandatoryObjective && (hasAMandatoryObjective = objective.canBeMandatory))
        {
            objective.ForceMandatory();
        }
    }

    public void UpdateObjectiveList()
    {
        string mandatory = "";
        string optional = "";
        int mandatoryCount = 0;
        foreach (BaseObjective objective in objectives)
        {
            string item = (objective.isMandatory ? "<color=\"yellow\">" : "") + "\n\t";
            item += (objective.isComplete ? "<s>" : "") + "- ";
            item += objective.objectiveTitle;
            item += objective.isComplete ? "</s>" : "";
            item += objective.isMandatory ? "</color=\"yellow\">" : "";

            if (objective.isMandatory)
            {
                mandatoryCount += objective.isComplete ? 0 : 1;
                mandatory += item;
            }
            else
            {
                optional += item;
            }
        }
        canFinish = mandatoryCount == 0;
        string extra = canFinish ? "\nMission Completed, escape through a cell tower." : "";
        GameManager.LevelManager.objectiveList.text = "Objective List:" + mandatory + optional + extra;
    }
}
