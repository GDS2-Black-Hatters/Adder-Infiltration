using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class UIButtonBehaviour : MonoBehaviour
{
    private enum ButtonType
    {
        ChangeLevelButton,
        ExitButton
    }
    [SerializeField] private ButtonType buttonType = ButtonType.ChangeLevelButton;
    private Button button;

    [Header("Start Game Parameters"), SerializeField] private string levelToGo;

    private void Start()
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
