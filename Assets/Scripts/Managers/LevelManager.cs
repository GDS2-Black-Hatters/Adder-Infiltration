#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager : MonoBehaviour, IManager
{
    private const int gameLevels = 100; //In case we somehow have over 100 different game scenes.

    //An enum of all the official scenes in the project.
    //Remember, all the states in the enum must be the same name as the actual scene.
    public enum Level
    {
        MainMenu,
        Hub,

        //Add game levels below this.
        Tutorial = gameLevels,
        Tutorial2,
        Tutorial3,

        //For Demo Purpose
        DemoProceduralLevel,

        Unknown, //This is for any unofficial levels.
    }
    public Level level { get; private set; }

    private (string transitionIn, string transitionOut) transitionType = ("FadeIn", "FadeOut");
    private (string feedbackIn, string feedbackOut) feedbackType = ("BoxSpinningIn", "BoxSpinningOut");
    private Animator transitionAnim;
    private bool isTransitioning = false;

    [SerializeField] private GameObject inGameHUD;
    private CaughtHUDBehaviour caughtHUD;

    [field: SerializeField] public TextMeshProUGUI objectiveList { get; private set; }

    public BaseSceneController ActiveSceneController { get; private set; }
    public Transform player { get; private set; }

    public void StartUp()
    {
        caughtHUD = inGameHUD.GetComponentInChildren<CaughtHUDBehaviour>();
        
        transitionAnim = GetComponentInChildren<Animator>();
        transitionAnim.updateMode = AnimatorUpdateMode.UnscaledTime;

        GameManager.VariableManager.timeToLive.onFinish += GameOver;
        GameManager.VariableManager.playerHealth.onDeath += GameOver;
    }

    public void OnApplicationFocus(bool focus)
    {
        bool lockMouse = focus && (int)level >= gameLevels && !PauseMenuController.GameIsPaused;
        Cursor.visible = !lockMouse;
        Cursor.lockState = lockMouse ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Start()
    {
        StartCoroutine(Transition(level, null, false));
        PlayLevelMusic();
    }

    private void PlayLevelMusic()
    {
        GameManager.AudioManager.PlayMusic(level switch
        {
            Level.Unknown => "",
            Level.Hub => "",
            Level.Tutorial => "",
            _ => ""
        });
    }

    /// <summary>
    /// Changes the scene accordingly.
    /// </summary>
    /// <param name="notify">A method called once the new scene has finished coding.</param>
    /// <param name="sceneName">The new scene to load.</param>
    public void ChangeLevel(Level level, Action notify)
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            //Add code here if you want to customise the transitions and loading stuff here.
            StartCoroutine(Transition(level, notify));
        }
    }

    /// <summary>
    /// Changes the scene accordingly.
    /// Use the other overload if delegates are needed.
    /// </summary>
    /// <param name="sceneName">The new scene to load.</param>
    public void ChangeLevel(Level level)
    {
        ChangeLevel(level, null);
    }

    private IEnumerator Transition(Level newLevel, Action notify = null, bool doTransitionIn = true)
    {
        IEnumerator LoadProgress(AsyncOperation async)
        {
            while (!async.isDone)
            {
                async.allowSceneActivation = async.progress >= 0.9f;
                yield return null;
            }
        }

        IEnumerator TransitionPlay(string transition)
        {
            transitionAnim.Play(transition);
            yield return new WaitForEndOfFrame();
            while (transitionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }
        }

        if (doTransitionIn)
        {
            yield return StartCoroutine(TransitionPlay(transitionType.transitionIn));
            yield return StartCoroutine(TransitionPlay(feedbackType.feedbackIn));

            if (level != newLevel)
            {
                yield return StartCoroutine(LoadProgress(SceneManager.LoadSceneAsync(DoStatic.EnumAsString(newLevel))));
            }
            notify?.Invoke();
            GameManager.SaveManager.SaveToFile();
            PlayLevelMusic();

            yield return StartCoroutine(TransitionPlay(feedbackType.feedbackOut));
        }
        
        OnApplicationFocus(true);
        caughtHUD.HideHUD();
        GameManager.VariableManager.Restart();
        UpdateLevelIndex();

        bool isNotLevel = (int)level < gameLevels;
        inGameHUD.SetActive(!isNotLevel);
        if (isNotLevel)
        {
            Camera.main.transform.eulerAngles = Vector3.zero;
            Camera.main.transform.position = new(0, 10, -10);
        }

        yield return StartCoroutine(TransitionPlay(transitionType.transitionOut));
        transitionAnim.Play("Waiting");
        isTransitioning = false;
    }

    private void UpdateLevelIndex()
    {
        try
        {
            level = DoStatic.StringToEnum<Level>(SceneManager.GetActiveScene().name);
        }
        catch
        {
            level = Level.Unknown;
        }
        OnApplicationFocus(true);
    }

    private void GameOver()
    {
        GameManager.LevelManager.ChangeLevel(Level.Hub);
    }

    public void SetActiveSceneController(BaseSceneController sceneController)
    {
        if (ActiveSceneController != null)
        {
            Debug.LogWarning("The previously active SceneController has not yet been destroyed, please ensure you are certain you want two SceneControllers active right now.");
        }
        ActiveSceneController = sceneController;
        ActiveSceneController.onPlayerDetection += caughtHUD.FadeIn;
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
}
