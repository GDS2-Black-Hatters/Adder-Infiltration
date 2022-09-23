using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool GameIsPaused;
    private CursorLockMode lastCursorLockMode;
    private bool cursorWasLocked;

    public GameObject OptionsPanel;
    public GameObject MainPanel;
    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
            Resume();
            }
            else
            {
            Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = lastCursorLockMode;
        Cursor.visible = cursorWasLocked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        lastCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        cursorWasLocked = Cursor.visible;
        Cursor.visible = true;
    }

    public void OpenOptionsPanel()
    {
        if(OptionsPanel != null && MainPanel != null)
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
        LevelManager levelManager = GameManager.LevelManager;
        levelManager.ChangeLevel(LevelManager.Level.Hub);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
