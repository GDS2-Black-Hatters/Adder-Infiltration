public class CollectObjective : BaseObjective
{
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
        objectiveTitle = $"Gather information ({collectCount.value}/{collectCount.originalValue})";
        if (collectCount.value == collectCount.originalValue)
        {
            Destroy(this);
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
