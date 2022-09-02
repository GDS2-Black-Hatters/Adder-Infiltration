#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IManager
{
    private (string transitionIn, string transitionOut) transitionType = ("FadeIn", "FadeOut");
    private (string feedbackIn, string feedbackOut) feedbackType = ("BoxSpinningIn", "BoxSpinningOut");
    private Animator transitionAnim;
    private bool isTransitioning = false;

    [field: SerializeField] public TextMeshProUGUI objectiveList { get; private set; }

    public BaseSceneController ActiveSceneController { get; private set; }
    public Transform player { get; private set; }

    public void StartUp()
    {
        transitionAnim = GetComponentInChildren<Animator>();
        transitionAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void OnApplicationFocus(bool focus)
    {
        Cursor.visible = !focus;
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Start()
    {
        StartCoroutine(Transition(SceneManager.GetActiveScene().name, null, false));
        PlayLevelMusic(SceneManager.GetActiveScene().name);
    }

    private void PlayLevelMusic(string levelName)
    {
        GameManager.AudioManager.PlayMusic(levelName switch
        {
            //Add scene names here
            _ => ""
        });
    }

    /// <summary>
    /// Changes the scene accordingly.
    /// </summary>
    /// <param name="notify">A method called once the new scene has finished coding.</param>
    /// <param name="sceneName">The new scene to load.</param>
    public void ChangeLevel(string sceneName, Action notify = null)
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            //Add code here if you want to customise the transitions and loading stuff here.
            StartCoroutine(Transition(sceneName, notify));
        }
    }

    private IEnumerator Transition(string newSceneName, Action notify = null, bool doTransitionIn = true)
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

            if (!SceneManager.GetActiveScene().name.Equals(newSceneName))
            {
                yield return StartCoroutine(LoadProgress(SceneManager.LoadSceneAsync(newSceneName)));
            }
            notify?.Invoke();
            GameManager.Save();
            PlayLevelMusic(SceneManager.GetActiveScene().name);

            yield return StartCoroutine(TransitionPlay(feedbackType.feedbackOut));
        }
        yield return StartCoroutine(TransitionPlay(transitionType.transitionOut));
        transitionAnim.Play("Waiting");
        isTransitioning = false;
    }

    public void SetActiveSceneController(BaseSceneController sceneController)
    {
        if (ActiveSceneController != null)
        {
            Debug.LogWarning("The previously active SceneController has not yet been destroyed, please ensure you are certain you want two SceneControllers active right now.");
        }
        ActiveSceneController = sceneController;
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
}
