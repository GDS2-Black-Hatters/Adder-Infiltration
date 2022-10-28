using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CaptureTheFlagObjective : BaseObjective
{
    [SerializeField] private float captureTime = 5f;
    [SerializeField] protected string prefixName = "Secure vulnerability";
    private TimeTracker timer;
    private bool withinRange = false;

    public UnityEngine.Events.UnityEvent<float> onProgressUpdate;
    [SerializeField] protected AK.Wwise.Event progressSFXEvent;

    protected override void Start()
    {
        base.Start();
        timer = new(captureTime, 1);
        UpdateName(0);
    }

    private void UpdateName(float progress)
    {
        objectiveTitle = $"{prefixName} ({progress * 100:00.0}%)";
        if (progress == 1)
        {
            ObjectiveFinish();
        }
    }

    void Update()
    {
        float progress = timer.Update(Time.deltaTime * (withinRange ? 1 : -1)) / timer.timer;
        onProgressUpdate.Invoke(progress);
        UpdateName(progress);
    }

    private void OnTriggerEnter(Collider other)
    {
        withinRange = other.CompareTag("Player") || withinRange;
        if (withinRange)
        {
            progressSFXEvent.Post(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        withinRange = !other.CompareTag("Player") && withinRange;
        if (!withinRange)
        {
            progressSFXEvent.Stop(gameObject);
        }
    }
}
