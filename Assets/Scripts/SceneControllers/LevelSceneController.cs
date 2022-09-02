/// <summary>
/// Should probably be renamed to PlayerTestController.
/// </summary>
public class LevelSceneController : BaseSceneController
{
    public UnityEngine.Events.UnityEvent OnPlayerDetection; //TEMP, REMOVE

    private void Start()
    {
        onPlayerDetection += TriggerPlayerDetection; //TEMP, REMOVE
    }

    protected void TriggerPlayerDetection()
    {
        OnPlayerDetection.Invoke();
	}
	
    protected override void Awake()
    {
        base.Awake();
	}
}
