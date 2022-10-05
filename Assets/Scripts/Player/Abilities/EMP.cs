using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : AbilityBase
{
    [SerializeField] private GameObject chargeSphere;
    [SerializeField] private GameObject effectSphere;

    [Header("Settings")]
    [SerializeField] private float maxRadius = 20;
    [SerializeField] private float chargeRadiusSpeed = 5;
    [SerializeField] private float effectRadialSpeed = 10;

    public override void ActivateAbility()
    {
        throw new System.NotImplementedException();
    }

    public override void EndAbilityPrime()
    {
        throw new System.NotImplementedException();
    }

    public override void StartAbilityPrime()
    {
        throw new System.NotImplementedException();
    }
}
