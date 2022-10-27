using System.Collections;
using TMPro;
using UnityEngine;
using static LevelManager.Level;

public class TimerTracker : CaughtVariableTracker
{
    private TextMeshProUGUI text;
    [SerializeField] private TimeTracker TimeToLive;
    [SerializeField] private Color full = Color.green;
    [SerializeField] private Color low = Color.red;

    protected override void Start()
    {
        base.Start();
        TimeToLive.onFinish += GameManager.LevelManager.OnDeath;

        text = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.LevelManager.ActiveSceneController.enemyAdmin.OnFullAlert += StartTimer;
    }

    private void StartTimer()
    {
        StartCoroutine(TimeTick());
    }

    private IEnumerator TimeTick()
    {
        TimeToLive.Reset(false);
        float tick;
        Lerper lerper = new();
        lerper.SetValues(1, 0.5f, 1);
        do
        {
            tick = TimeToLive.Update(Time.deltaTime);
            float percentage = tick / TimeToLive.timer;
            ui.fillAmount = Mathf.Lerp(1, 0.5f, 1 - percentage);
            ui.color = Color.Lerp(low, full, percentage);

            int minutes = (int)(tick / 60);
            float seconds = tick - (60 * minutes);
            text.text = minutes > 0 ? $"{minutes:0}:{seconds:00}" : $"{seconds:#0.00}s";
            yield return null;
        } while (TimeToLive.TimePercentage != 0);
    }
}
