#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private CaughtHUDBehaviour caughtHUD;
    [field: SerializeField] public TextMeshProUGUI objectiveList { get; private set; }

    //For children.
    protected List<BaseObjective> objectives = new();
    [SerializeField] private AK.Wwise.Event playerDetectionMusicEvent; //Private? Under the for children section?

    //Skybox Lerping.
    [Header("Skybox Parameters")]
    //[SerializeField] private Color startColor = Color.green;
    //[SerializeField] private Color endColor = Color.red;
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
        enemyAdmin.onFullAlert += caughtHUD.FadeIn;
    }

    protected virtual void Update()
    {
        UpdateObjectiveList();

        //Fall Off Check
        LevelManager levelManager = GameManager.LevelManager;
        if (levelManager.player.transform.position.y < -15f)
        {
            levelManager.ChangeLevel(LevelManager.Level.Hub);
        }
    }

    public void AddSpawnPoint(Transform transform)
    {
        playerSpawnPoints.Add(transform);
    }

    public void SetSpawnPoint()
    {
        GameManager.LevelManager.player.transform.position = playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Count)].position;
    }

    public void StartCaughtMode()
    {
        if (sceneMode == SceneState.Stealth)
        {
            sceneMode = SceneState.Combat;
            playerDetectionMusicEvent.Post(gameObject);
            StartCoroutine(LerpSkybox());
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
        objectiveList.text = $"Objective List:{mandatory}{optional}{extra}";
    }

    private IEnumerator LerpSkybox()
    {
        lerper.SetValues(0, 1, lerpTime);
        do
        {
            lerper.Update(Time.deltaTime);
            //RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(startColor, endColor, lerper.currentValue));
            RenderSettings.skybox.SetFloat("_LerpState", lerper.currentValue);
            yield return null;
        } while (lerper.isLerping);
    }
}
