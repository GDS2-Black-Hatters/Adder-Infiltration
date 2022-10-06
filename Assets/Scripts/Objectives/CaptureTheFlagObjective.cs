using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CaptureTheFlagObjective : BaseObjective
{
    [SerializeField] private float captureTime = 5f;
    private TimeTracker timer;
    private bool withinRange = false;
    [SerializeField] protected string prefixName = "Secure vulnerability";
    private string _objectiveTitle;
    public override string objectiveTitle { get { return _objectiveTitle; }}

    public UnityEngine.Events.UnityEvent<float> onProgressUpdate;

    protected override void Start()
    {
        base.Start();
        timer = new(captureTime, 1);
        UpdateName(0);
    }

    private float UpdateTimer()
    {
        return timer.Update(Time.deltaTime * (withinRange ? 1 : -1)) / timer.timer;
    }

    private void UpdateProgress(float progress)
    {
        onProgressUpdate.Invoke(progress);
    }

    private void UpdateName(float progress)
    {
        _objectiveTitle = $"{prefixName} ({progress * 100:00.0}%)";
        if (progress == 1)
        {
            ObjectiveCompleteSound.Post(gameObject);
            Destroy(this);
        }
    }

    void Update()
    {
        float progress = UpdateTimer();
        UpdateProgress(progress);
        UpdateName(progress);
    }

    private void OnTriggerEnter(Collider other)
    {
        withinRange = other.CompareTag("Player") || withinRange;
    }

    private void OnTriggerExit(Collider other)
    {
        withinRange = !other.CompareTag("Player") && withinRange;
    }
}
