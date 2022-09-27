using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StareAndSpin))]
public class WeaponMatter : Matter
{
    MatterShell ownerMatterShell;

    private RandRotate anchorRandRoter;
    private StareAndSpin anchorStareAndSpin;
    private RandRotate selfRandRoter;
    private StareAndSpin selfStareAndSpin;

    protected Transform target; 


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

    protected virtual void Update()
    {
        AttackUpdate();
    }

    protected virtual void AttackUpdate(){}

    public virtual void ChangeTarget(Transform newTargetTransform)
    {
        //set target
        target = newTargetTransform;

        //change orbit behaviou depending on if a target exist
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
