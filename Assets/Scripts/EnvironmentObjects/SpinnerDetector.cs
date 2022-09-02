using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerDetector : DetectorEnvironmentObject
{
    public override void PlayerInRange()
    {
        OnDetect.Invoke();
    }
}
