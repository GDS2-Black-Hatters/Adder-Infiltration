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
    [Header("Toggle GameObject Parameters"), SerializeField] private GameObject gameObjectToToggle;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonType switch
        {
            ButtonType.ChangeLevelButton => ChangeLevel,
            ButtonType.ToggleGameObject => ToggleGameObject,
            ButtonType.ExitButton => ExitGame,
            _ => UnknownButton,
        });
    }

    private void ChangeLevel()
    {
        GameManager.LevelManager.ChangeLevel(levelToGo);
    }

    private void ToggleGameObject()
    {
        gameObjectToToggle.SetActive(!gameObjectToToggle.activeInHierarchy);
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
