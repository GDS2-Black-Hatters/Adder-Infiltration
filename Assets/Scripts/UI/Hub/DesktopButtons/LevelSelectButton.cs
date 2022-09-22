using UnityEngine;

public class LevelSelectButton : BaseDesktopButton
{
    [SerializeField] private LevelManager.Level level = LevelManager.Level.Tutorial;

    protected override void OnClick()
    {
        GameManager.LevelManager.ChangeLevel(level);
    }
}
