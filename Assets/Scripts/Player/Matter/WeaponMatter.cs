using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMatter : Matter
{
    RandRotate anchorRandRoter;
    RandRotate selfRandRoter;

    public override void InitilizeMatter(Transform anchorBase)
    {
        anchorRandRoter = anchorBase.GetComponent<RandRotate>();
        selfRandRoter = GetComponent<RandRotate>();
        anchorRandRoter.rotationSpeed = 0;
    }
}
