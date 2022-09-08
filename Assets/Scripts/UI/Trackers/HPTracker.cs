public class HPTracker : VariableTracker
{
    protected override void UpdateUI()
    {
        //Get Current HP.
        if (!GameManager.LevelManager.ActiveSceneController || GameManager.LevelManager.ActiveSceneController.sceneMode == BaseSceneController.SceneState.Stealth)
        {
            return;
        }

        ui.fillAmount = GameManager.VariableManager.playerHealth.healthPercentage;
    }
}
