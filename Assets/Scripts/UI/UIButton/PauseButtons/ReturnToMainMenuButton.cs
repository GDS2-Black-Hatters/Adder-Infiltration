using static LevelManager;
using UnityEngine;

public class ReturnToMainMenuButton : BaseButton
{
    protected override void OnClick()
    {
        Time.timeScale = 1f;
        GameManager.LevelManager.ChangeLevel(Level.Hub);
    }
}
