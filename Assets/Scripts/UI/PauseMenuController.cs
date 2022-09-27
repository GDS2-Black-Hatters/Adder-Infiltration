using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallBackContext;
using static InputManager.ControlScheme;
using static LevelManager;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private CursorLockMode lastCursorLockMode;
    private bool cursorWasLocked;

    public GameObject OptionsPanel;
    public GameObject MainPanel;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGame, GameManager.InputManager.GetAction(InputManager.Controls.Pause), Performed, TogglePause)
        });
    }

    private void TogglePause(InputAction.CallbackContext moveDelta)
    {
        //Todo: maybe optimise later.
        if (isGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;

        Cursor.lockState = lastCursorLockMode;
        Cursor.visible = cursorWasLocked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;

        lastCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;

        cursorWasLocked = Cursor.visible;
        Cursor.visible = true;
    }

    public void OpenOptionsPanel()
    {
        if (OptionsPanel != null && MainPanel != null)
        {
            //Get Active status
            bool optionIsActive = OptionsPanel.activeSelf;
            bool mainIsActive = MainPanel.activeSelf;

            //Set Active & Not Active
            OptionsPanel.SetActive(!optionIsActive);
            MainPanel.SetActive(!mainIsActive);
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        LevelManager levelManager = GameManager.LevelManager;
        levelManager.ChangeLevel(Level.Hub);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
