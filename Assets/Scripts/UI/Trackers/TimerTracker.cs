using TMPro;
using UnityEngine;

public class TimerTracker : VariableTracker
{
    private TextMeshProUGUI text;
    [SerializeField] private Color full = Color.green;
    [SerializeField] private Color low = Color.red;

    protected override void Start()
    {
        base.Start();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void UpdateUI()
    {
        if (GameManager.LevelManager.ActiveSceneController.sceneMode != BaseSceneController.SceneState.Combat)
        {
            return;
        }

        TimeTracker tracker = GameManager.VariableManager.timeToLive;
        float tick = tracker.Update(Time.deltaTime);
        float percentage = tick / tracker.timer;
        if (tick == 0)
        {
            tracker.Reset();
        }

        ui.fillAmount = percentage;
        ui.color = Color.Lerp(low, full, percentage);

        int minutes = (int)(tick / 60);
        float seconds = tick - (60 * minutes);
        text.text = minutes > 0 ? minutes.ToString("0") + ":" + seconds.ToString("00") : seconds.ToString("#0.00") + "s";
    }
}
