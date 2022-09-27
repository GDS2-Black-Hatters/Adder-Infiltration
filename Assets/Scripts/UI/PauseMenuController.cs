using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallBackContext;
using static InputManager.ControlScheme;
using static InputManager.Controls;
using static LevelManager;

public class PauseMenuController : MonoBehaviour
{
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
            new(MainGame, GameManager.InputManager.GetAction(Pause), Performed, TogglePause),
        });
    }

    private void TogglePause(InputAction.CallbackContext moveDelta)
    {
        isGamePaused = !isGamePaused;
        GameManager.LevelManager.OnApplicationFocus(!isGamePaused);
        canvasGroup.alpha = 1 - canvasGroup.alpha;
        canvasGroup.interactable = isGamePaused;
        Time.timeScale = 1 - Time.timeScale;

        mainPanel.SetActive(isGamePaused);
        optionsPanel.SetActive(false);
    }

    public void TogglePause()
    {
        TogglePause(new());
    }
}
