using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class UIButtonBehaviour : MonoBehaviour
{
    protected enum ButtonType
    {
        ChangeLevelButton,
        ExitButton
    }
    [SerializeField] protected ButtonType buttonType = ButtonType.ChangeLevelButton;
    protected Button button;

    [Header("Start Game Parameters"), SerializeField] private LevelManager.Level levelToGo;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonType switch
        {
            ButtonType.ChangeLevelButton => ChangeLevel,
            ButtonType.ExitButton => ExitGame,
            _ => throw new System.NotImplementedException(),
        });
    }
    private void ChangeLevel()
    {
        GameManager.LevelManager.ChangeLevel(levelToGo);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
