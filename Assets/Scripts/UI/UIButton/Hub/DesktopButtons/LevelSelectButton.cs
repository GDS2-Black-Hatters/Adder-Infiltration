public class LevelSelectButton : BaseDesktopButton
{
    protected override void OnClick()
    {
        //For now:
        GameManager.LevelManager.ChangeLevel(LevelManager.Level.Tutorial);
    }
}
