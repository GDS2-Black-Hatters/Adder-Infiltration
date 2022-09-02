using UnityEngine;

public class StalkingObjective : CaptureTheFlagObjective
{
    //To make it a bit more fair, when caught, increase the size of
    [SerializeField] private float sizeMultiplier = 2f;

    protected override void Start()
    {
        base.Start();
        prefixName = "Observe security behaviour";
        GameManager.LevelManager.ActiveSceneController.onPlayerDetection += WhenCaught;
    }

    private void WhenCaught()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale *= sizeMultiplier;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.LevelManager.ActiveSceneController.onPlayerDetection -= WhenCaught;
    }
}
