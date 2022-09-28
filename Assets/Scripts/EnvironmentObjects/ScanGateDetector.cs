public class ScanGateDetector : DetectorEnvironmentObject
{
    public override void PlayerInRange()
    {
        OnDetect.Invoke();
    }
}
