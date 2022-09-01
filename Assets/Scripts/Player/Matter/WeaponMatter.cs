using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMatter : Matter
{
    MatterShell ownerMatterShell;

    private RandRotate anchorRandRoter;
    private RandRotate selfRandRoter;

    private float anchorRotSpeed;
    private float selfRotSpeed;

    public override void InitilizeMatter(MatterShell ownerShell, Transform anchorBase)
    {
        anchorRandRoter = anchorBase.GetComponent<RandRotate>();
        selfRandRoter = GetComponent<RandRotate>();
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
            return;
        }

        anchorRandRoter.rotationSpeed = 0;
        selfRandRoter.rotationSpeed = 0;            
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
