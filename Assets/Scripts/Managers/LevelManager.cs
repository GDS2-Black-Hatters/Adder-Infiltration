#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static InputManager.ControlScheme;

public sealed class LevelManager : BaseManager
{
    #region Levels
    private const string loadSceneName = "LoadingScene";
    private const int gameLevels = 100; //In case we somehow have over 100 different game scenes.
    private const int longLoadLevels = 200; //For levels that have longer load times we throw in the fancy load screen.
    //An enum of all the official scenes in the project.
    //Remember, all the states in the enum must be the same name as the actual scene.
    public enum Level
    {
        MainMenu,
        Hub,

        //Add game levels below this.
        Tutorial = gameLevels,
        Tutorial2,
        Campaign1,
        Campaign2,
        Campaign3,
        Campaign4,
        Campaign5,

        //Anything below this will be given the long load scene and will require a release call to get out of the loading scene
        StandardProceduralLevel = longLoadLevels,
        ProceduralLevel,
        Unknown, //This is for any unofficial levels.
    }
    public Level level { get; private set; }
    #endregion

    public static bool isGamePaused { get; private set; }
    public Action<bool> onGamePauseStateChange;
    public Action onGamePause;
    public Action onGameResume;

    private Scene fancyLoadingScene;
    public bool loadingSceneReleased { get; private set; }
    public Action onLoadSceneTransitionOut;

    private (string transitionIn, string transitionOut) transitionType = ("FadeIn", "FadeOut");
    private (string feedbackIn, string feedbackOut) feedbackType = ("BoxSpinningIn", "BoxSpinningOut");
    private Animator transitionAnim;
    private bool isTransitioning = false;

    public BaseSceneController ActiveSceneController { get; private set; }

    public override BaseManager StartUp()
    {
        transitionAnim = GetComponentInChildren<Animator>();
        transitionAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
        return this;
    }

    /// <summary>
    /// Toggles the state of the mouse.
    /// </summary>
    /// <param name="focus">True to hide the mouse and lock it in place.</param>
    public void OnApplicationFocus(bool focus)
    {
        bool lockMouse = focus && (int)level >= gameLevels && !isGamePaused;
        Cursor.visible = !lockMouse;
        Cursor.lockState = lockMouse ? CursorLockMode.Locked : CursorLockMode.None;
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

    public void OnDeath()
    {
        if (GameManager.VariableManager.GetVariable<bool>(SaveManager.VariableToSave.tutorialFinish))
        {
            GameManager.LevelManager.ChangeLevel(Level.Hub);
        }
        else if(!isTransitioning)
        {
            isTransitioning = true;
            StartCoroutine(Transition(level, forceRestart: true));
        }
    }

    private IEnumerator Transition(Level newLevel, Action notify = null, bool doTransitionIn = true, bool forceRestart = false)
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

            if (level != newLevel || forceRestart)
            {
                //if the level requires a long load time, yield return the loading scene first, then start loading the actual scene async in the background.
                if((int)newLevel >= longLoadLevels)
                {
                    yield return StartCoroutine(LoadProgress(SceneManager.LoadSceneAsync(loadSceneName)));
                    SceneManager.LoadSceneAsync(DoStatic.EnumToString(newLevel), LoadSceneMode.Additive);
                }
                else
                {
                    yield return StartCoroutine(LoadProgress(SceneManager.LoadSceneAsync(DoStatic.EnumToString(newLevel))));                
                }
            }
            notify?.Invoke();
            GameManager.SaveManager.SaveToFile();

            yield return StartCoroutine(TransitionPlay(feedbackType.feedbackOut));
        }

        SetIsGamePaused(false);
        OnApplicationFocus(true);
        UpdateLevelIndex();

        bool isNotInGame = (int)level < gameLevels;
        if (isNotInGame) //Camera Reset
        {
            Camera.main.transform.eulerAngles = Vector3.zero;
            Camera.main.transform.position = new(0, 10, -10);
        }
        GameManager.InputManager.SetControlScheme(isNotInGame ? Hub : MainGame);

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

    public void SetIsGamePaused(bool isGamePaused)
    {
        if (LevelManager.isGamePaused == isGamePaused)
        {
            return;
        }

        LevelManager.isGamePaused = isGamePaused;
        if (isGamePaused)
        {
            onGamePause?.Invoke();
        }
        else
        {
            onGameResume?.Invoke();
        }
        onGamePauseStateChange?.Invoke(isGamePaused);
    }

    public void SetActiveSceneController(BaseSceneController sceneController)
    {
        if (ActiveSceneController != null)
        {
            Debug.LogWarning("The previously active SceneController has not yet been destroyed, please ensure you are certain you want two SceneControllers active right now.");
        }
        ActiveSceneController = sceneController;
    }

    public void ReleaseLoadScene()
    {
        loadingSceneReleased = true;
        onLoadSceneTransitionOut?.Invoke();
    }
}
