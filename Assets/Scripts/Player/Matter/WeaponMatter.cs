using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMatter : Matter
{
    MatterShell ownerMatterShell;

    private RandRotate anchorRandRoter;
    private StareAndSpin anchorStareAndSpin;
    private RandRotate selfRandRoter;
    private StareAndSpin selfStareAndSpin;

    public override void InitilizeMatter(MatterShell ownerShell, Transform anchorBase)
    {
        anchorRandRoter = anchorBase.GetComponent<RandRotate>();
        anchorStareAndSpin = anchorBase.GetComponent<StareAndSpin>();
        selfRandRoter = GetComponent<RandRotate>();
        selfStareAndSpin = GetComponent<StareAndSpin>();
        
        ownerMatterShell = ownerShell;
        ChangeTarget(ownerMatterShell.currentTarget);
        ownerMatterShell.OnTargetChange += ChangeTarget;
    }

    public virtual void ChangeTarget(Transform newTargetTransform)
    {
        bool hasTarget = !(newTargetTransform == null);

        anchorRandRoter.enabled = !hasTarget;
        selfRandRoter.enabled = !hasTarget;
            
        anchorStareAndSpin.enabled = hasTarget;
        selfStareAndSpin.enabled = hasTarget;

        anchorStareAndSpin.stareTarget = newTargetTransform;
        selfStareAndSpin.stareTarget = newTargetTransform;
    }

    private void OnEnable()
    {
        if(ownerMatterShell == null) return;
        ownerMatterShell.OnTargetChange += ChangeTarget;
    }

    private void OnDisable()
    {
        ownerMatterShell.OnTargetChange -= ChangeTarget;
    }
}
