using UnityEngine;

public class HPTracker : VariableTracker
{
    protected override void UpdateUI()
    {
        //Get Current HP.
        if (GameManager.LevelManager.ActiveSceneController.sceneMode == BaseSceneController.SceneState.Stealth)
        {
            return;
        }

        Health health = GameManager.VariableManager.playerHealth;
        health.Update(Time.deltaTime);
        float percentage = health.healthPercentage;
        //If percentage is 0, player ded.
        ui.fillAmount = percentage;
    }
}
