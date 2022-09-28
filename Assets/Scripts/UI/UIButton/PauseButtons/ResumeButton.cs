using UnityEngine;

public class ResumeButton : BaseButton
{
    [SerializeField] private PauseMenuController pauseMenuController;

    protected override void OnClick()
    {
        pauseMenuController.TogglePause();
    }
}
