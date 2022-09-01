using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CaptureTheFlagObjective : BaseObjective
{
    [SerializeField] private float captureTime = 5f;
    private TimeTracker timer;
    private bool withinRange = false;
    protected string prefixName = "Secure vulnerability";

    protected override void Start()
    {
        base.Start();
        timer = new(captureTime, 1);
        UpdateName();
    }

    private void UpdateName()
    {
        timer.Update(Time.deltaTime * (withinRange ? 1 : -1));
        float percentage = timer.tick / timer.timer;
        objectiveTitle = $"{prefixName} ({percentage * 100:00.0}%)";
        GameManager.LevelManager.ActiveSceneController.UpdateObjectiveList();
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
