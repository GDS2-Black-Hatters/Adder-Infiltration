using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanGateDetector : DetectorEnvironmentObject
{
    public override void PlayerInRange()
    {
        OnDetect.Invoke();
    }
}
