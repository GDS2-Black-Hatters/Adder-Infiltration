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

    protected override void Start()
    {
        base.Start();
        timer = new(captureTime, 1);
        UpdateName();
    }

    private void UpdateName()
    {
        float percentage = timer.Update(Time.deltaTime * (withinRange ? 1 : -1)) / timer.timer;
        _objectiveTitle = $"{prefixName} ({percentage * 100:00.0}%)";
        if (percentage == 1)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        UpdateName();
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
