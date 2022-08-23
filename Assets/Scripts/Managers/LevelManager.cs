using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private (string transitionIn, string transitionOut) transitionType = ("FadeIn", "FadeOut");
    private (string feedbackIn, string feedbackOut) feedbackType = ("BoxSpinningIn", "BoxSpinningOut");
    private Animator transitionAnim;
    private bool isTransitioning = false;

    private void Awake()
    {
        transitionAnim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Transition(SceneManager.GetActiveScene().name, null, false));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            ChangeLevel(sceneName.Equals("Login Screen") ? "DummyTest" : "Login Screen");
        }
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
    public void ChangeLevel(string sceneName, DoStatic.SimpleDelegate notify = null)
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            //Add code here if you want to customise the transitions and loading stuff here.
            StartCoroutine(Transition(sceneName, notify));
        }
    }

    private IEnumerator Transition(string newSceneName, DoStatic.SimpleDelegate notify = null, bool doTransitionIn = true)
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
        }

        yield return StartCoroutine(TransitionPlay(feedbackType.feedbackIn));

        if (!SceneManager.GetActiveScene().name.Equals(newSceneName))
        {
            yield return StartCoroutine(LoadProgress(SceneManager.LoadSceneAsync(newSceneName)));
        }

        notify?.Invoke();
        PlayLevelMusic(SceneManager.GetActiveScene().name);

        yield return StartCoroutine(TransitionPlay(feedbackType.feedbackOut));
        yield return StartCoroutine(TransitionPlay(transitionType.transitionOut));
        transitionAnim.Play("Waiting");
        isTransitioning = false;
    }
}