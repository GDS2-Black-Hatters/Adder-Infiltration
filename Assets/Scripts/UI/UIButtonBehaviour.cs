using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class UIButtonBehaviour : MonoBehaviour
{
    protected enum ButtonType
    {
        ChangeLevelButton,
        ToggleGameObject,
        ExitButton
    }
    [SerializeField] protected ButtonType buttonType = ButtonType.ChangeLevelButton;
    protected Button button;

    [Header("Start Game Parameters"), SerializeField] private LevelManager.Level levelToGo;
    [Header("Toggle Desktop GameObject Parameters"), SerializeField] private DesktopWindowApplication applicationToToggle;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonType switch
        {
            ButtonType.ChangeLevelButton => ChangeLevel,
            ButtonType.ToggleGameObject => ToggleDesktopApplication,
            ButtonType.ExitButton => ExitGame,
            _ => UnknownButton,
        });

        if (applicationToToggle != null)
        {
            applicationToToggle.SetStartPos(transform.position);
        }
    }

    private void ChangeLevel()
    {
        GameManager.LevelManager.ChangeLevel(levelToGo);
    }

    private void ToggleDesktopApplication()
    {
        applicationToToggle.ToggleApplication();
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void UnknownButton()
    {
        Debug.Log("Unknown button, how'd you even get here?!");
    }
}
