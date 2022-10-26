using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;
using static LevelManager;

public class PauseMenuController : MonoBehaviour
{
    private float prevScale;
    private CanvasGroup canvasGroup;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;

    // Start is called before the first frame update
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGamePause, Performed, TogglePause),
        });
    }

    private void TogglePause(InputAction.CallbackContext moveDelta)
    {
        GameManager.LevelManager.SetIsGamePaused(!isGamePaused);
        GameManager.LevelManager.OnApplicationFocus(!isGamePaused);
        canvasGroup.alpha = 1 - canvasGroup.alpha;
        canvasGroup.interactable = isGamePaused;

        if (isGamePaused)
        {
            prevScale = Time.timeScale;
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = prevScale;
        }

        mainPanel.SetActive(isGamePaused);
        optionsPanel.SetActive(false);
    }

    public void TogglePause()
    {
        TogglePause(new());
    }
}
