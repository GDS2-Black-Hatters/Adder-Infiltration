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

    private float anchorRotSpeed;
    private float selfRotSpeed;

    public override void InitilizeMatter(MatterShell ownerShell, Transform anchorBase)
    {
        anchorRandRoter = anchorBase.GetComponent<RandRotate>();
        anchorStareAndSpin = anchorBase.GetComponent<StareAndSpin>();
        selfRandRoter = GetComponent<RandRotate>();
        selfStareAndSpin = GetComponent<StareAndSpin>();
        anchorRotSpeed = anchorRandRoter.rotationSpeed;
        selfRotSpeed = selfRandRoter.rotationSpeed;
        
        ownerMatterShell = ownerShell;
        ChangeTarget(ownerMatterShell.currentTarget);
        ownerMatterShell.OnTargetChange += ChangeTarget;
    }

    public void ChangeTarget(Transform newTargetTransform)
    {
        if(newTargetTransform == null)
        {
            anchorRandRoter.rotationSpeed = anchorRotSpeed;
            selfRandRoter.rotationSpeed = selfRotSpeed;
            
            anchorStareAndSpin.enabled = false;
            selfStareAndSpin.enabled = false;

            return;
        }

        anchorRandRoter.rotationSpeed = 0;
        selfRandRoter.rotationSpeed = 0;            

        anchorStareAndSpin.stareTarget = newTargetTransform;
        anchorStareAndSpin.enabled = true;
        selfStareAndSpin.stareTarget = newTargetTransform;
        selfStareAndSpin.enabled = true;
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
