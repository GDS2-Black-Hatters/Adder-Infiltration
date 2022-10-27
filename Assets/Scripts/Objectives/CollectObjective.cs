using UnityEngine;

public class CollectObjective : BaseObjective
{
    [SerializeField] private string prefixName = "Gather information";
    private OriginalValue<int> collectCount;
    private int prevCount;

    protected override void Start()
    {
        base.Start();
        collectCount = new(transform.childCount);
        UpdateName();
    }

    private void UpdateName()
    {
        prevCount = transform.childCount;
        collectCount.value = collectCount.originalValue - transform.childCount;
        objectiveTitle = $"{prefixName} ({collectCount.value}/{collectCount.originalValue})";
        if (collectCount.value == collectCount.originalValue)
        {
            ObjectiveFinish();
        }
    }

    private void Update()
    {
        if (prevCount != transform.childCount)
        {
            UpdateName();
        }
    }
}
