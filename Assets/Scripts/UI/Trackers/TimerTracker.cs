using System.Collections;
using TMPro;
using UnityEngine;

public class TimerTracker : CaughtVariableTracker
{
    private TextMeshProUGUI text;
    [SerializeField] private Color full = Color.green;
    [SerializeField] private Color low = Color.red;

    protected override void Start()
    {
        base.Start();
        text = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.LevelManager.ActiveSceneController.enemyAdmin.onFullAlert += StartUI;
    }

    private void StartUI()
    {
        StartCoroutine(TimeTick());
    }

    private IEnumerator TimeTick()
    {
        TimeTracker tracker = GameManager.VariableManager.timeToLive;
        float tick;
        Lerper lerper = new();
        lerper.SetValues(1, 0.5f, 1);
        do
        {
            tick = tracker.Update(Time.deltaTime);
            float percentage = tick / tracker.timer;
            ui.fillAmount = lerper.ValueAtPercentage(1 - percentage);
            ui.color = Color.Lerp(low, full, percentage);

            int minutes = (int)(tick / 60);
            float seconds = tick - (60 * minutes);
            text.text = minutes > 0 ? $"{minutes:0}:{seconds:00}" : $"{seconds:#0.00}s";
            yield return null;
        } while (tick != 0);
        tracker.Reset();
    }
}
