using UnityEngine;

public class TimerTracker : VariableTracker
{
    protected override void UpdateUI()
    {
        TimeTracker tracker = GameManager.VariableManager.timeToLive;
        tracker.Update(Time.deltaTime);
        ui.fillAmount = tracker.tick / tracker.timer;
    }
}
