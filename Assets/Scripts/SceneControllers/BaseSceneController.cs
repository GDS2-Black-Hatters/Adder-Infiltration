#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static LevelManager.Level;

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
    private readonly List<Transform> playerSpawnPoints = new();
    private ColorAdjustments colorAdjustments;
    private readonly List<CanvasGroup> iconCanvases = new();

    [SerializeField] private CaughtHUDBehaviour caughtHUD;
    [field: SerializeField] public PlayerVirus Player { get; private set; }
    [field: SerializeField] public TextMeshProUGUI objectiveList { get; private set; }

    //For children.
    protected List<BaseObjective> objectives = new();
    [SerializeField] private AK.Wwise.Event playerDetectionMusicEvent; //Private? Under the for children section?

    //Skybox Lerping.
    [Header("Skybox Parameters")]
    [SerializeField] private float lerpTime = 1f;
    private readonly Lerper lerper = new();

    //MatterShell
    private MatterShell matterShell;

    [SerializeField] protected WireframeLoadEffectRig loadEffectRig;

    protected virtual void Awake()
    {
        GameManager.LevelManager.SetActiveSceneController(this);

        VolumeProfile volumeProfile = GetComponent<Volume>().profile;
        if (!volumeProfile.TryGet(out colorAdjustments))
        {
            colorAdjustments = volumeProfile.Add<ColorAdjustments>(false);
        }
    }

    protected virtual void Start()
    {
        //Ensure we take over the 'active scene' status from the loading scene
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(gameObject.scene);

        //Create and assign a copy so we don't change the asset original values
        RenderSettings.skybox = new(RenderSettings.skybox);

        enemyAdmin.OnFullAlert += StartCaughtMode;
        enemyAdmin.OnFullAlert += caughtHUD.FadeIn;
    }

    public virtual void EngageScene()
    {
        loadEffectRig.StartRig();
    }

    protected virtual void Update()
    {
        UpdateObjectiveList();

        //Fall Off Check
        if (Player.transform.position.y < -15f)
        {
            GameManager.LevelManager.ChangeLevel(Hub);
        }
    }

    public void AddSpawnPoint(Transform transform)
    {
        playerSpawnPoints.Add(transform);
    }

    public void SetSpawnPoint()
    {
        GameManager.LevelManager.ActiveSceneController.Player.transform.position = playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Count)].position;
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
    }

    public void UpdateObjectiveList()
    {
        if (canFinish)
        {
            return;
        }

        //StringBuilder is apparently way faster than any string append and such.
        StringBuilder item = new("Objective List:");

        int unfinishedCount = 0;
        foreach (BaseObjective objective in objectives)
        {
            unfinishedCount += objective.isComplete ? 0 : 1;
            item.Append("\n\t");
            item.Append(objective.isComplete ? "<s>" : "");
            item.Append("- ");
            item.Append(objective.objectiveTitle);
            item.Append(objective.isComplete ? "</s>" : "");
        }
        canFinish = unfinishedCount == 0;
        item.Append(canFinish ? "\nMission Completed, escape through a cell tower." : "");
        objectiveList.text = item.ToString();
    }

    private IEnumerator LerpSkybox()
    {
        lerper.SetValues(0, 1, lerpTime);
        do
        {
            lerper.Update(Time.deltaTime);
            RenderSettings.skybox.SetFloat("_LerpState", lerper.currentValue);
            yield return null;
        } while (lerper.isLerping);
    }

    #region Scan Ability
    public void StartScan()
    {
        StartCoroutine(LerpObjectiveIcons());
    }

    public void AddIconCanvas(CanvasGroup group)
    {
        iconCanvases.Add(group);
    }

    private IEnumerator LerpObjectiveIcons()
    {
        IEnumerator Wait(float duration, Action<float> action)
        {
            bool isRunning = true;
            TimeTracker time = new(duration, 1);
            time.onFinish += () =>
            {
                isRunning = false;
            };

            do
            {
                yield return null;
                if (!LevelManager.isGamePaused)
                {
                    time.Update(Time.unscaledDeltaTime);
                    action?.Invoke(time.TimePercentage);
                }
            } while (isRunning);
        }

        void IconUpdate(float alpha)
        {
            foreach (CanvasGroup group in iconCanvases)
            {
                if (group)
                {
                    group.alpha = alpha;
                }
            }
        }

        yield return StartCoroutine(Wait(0.5f, (percentage) =>
        {
            Time.timeScale = 1 - percentage * 0.5f;
            colorAdjustments.saturation.value = -100 * percentage;
            IconUpdate(percentage);
        }));

        DurationAbility ability = (DurationAbility)GameManager.VariableManager.GetAbility(VariableManager.AllAbilities.Scan);
        Upgradeable upgrade = (Upgradeable)GameManager.VariableManager.GetUnlockable(VariableManager.AllUnlockables.Scan);
        yield return StartCoroutine(Wait(ability.AbilityDuration.GetCurrentValue(upgrade.UnlockProgression) - 1, null)); //-1 to account lerp in and out.
        yield return StartCoroutine(Wait(0.5f, (percentage) =>
        {
            Time.timeScale = 0.5f + percentage * 0.5f;
            float percent = 1 - percentage;
            colorAdjustments.saturation.value = -100 * percent;
            IconUpdate(percent);
        }));
    }
    #endregion
}
