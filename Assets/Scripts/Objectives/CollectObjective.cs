public class CollectObjective : BaseObjective
{
    [UnityEngine.SerializeField] private string _objectiveStringPrefix = "Gather information";
    public override string objectiveTitle { get { return $"{_objectiveStringPrefix} ({collectCount.value}/{collectCount.originalValue})"; }}

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
