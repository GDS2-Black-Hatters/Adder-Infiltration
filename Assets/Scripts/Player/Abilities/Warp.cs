using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : AbilityBase
{
    [SerializeField] private GameObject warpPointMarker;
    [SerializeField] private float warpMaxDistance = 30;
    [SerializeField] private LayerMask warpTargetMask;
    private const float warpRadius = 1.6f;

    private Coroutine warpPointMarkCoroutine;
    private bool validWarpTargetPosition;

    private void Start()
    {
        warpPointMarker.SetActive(false);
    }

    public override void ActivateAbility()
    {
        if(!validWarpTargetPosition)
        {
            return;
        }

        GameManager.LevelManager.player.transform.position = warpPointMarker.transform.position + Vector3.up * GameManager.LevelManager.player.transform.position.y;
        EndAbilityPrime();
    }

    public override void EndAbilityPrime()
    {
        if(warpPointMarkCoroutine != null)
        {
            StopCoroutine(warpPointMarkCoroutine);
        }

        validWarpTargetPosition = false;
        warpPointMarker.SetActive(false);
    }

    public override void StartAbilityPrime()
    {
        if(warpPointMarkCoroutine != null)
        {
            StopCoroutine(warpPointMarkCoroutine);
        }

        warpPointMarker.SetActive(true);

        warpPointMarkCoroutine = StartCoroutine(updateWarpPointMark());
    }

    private IEnumerator updateWarpPointMark()
    {
        RaycastHit castResult;
        Vector3 sphereCenter;
        while(true)
        {
            if(Physics.SphereCast(Camera.main.transform.position, warpRadius, Camera.main.transform.forward, out castResult, warpMaxDistance, warpTargetMask))
            {
                sphereCenter = castResult.point + warpRadius * castResult.normal;
            }
            else
            {                
                //sphereCenter = Camera.main.transform.position + Camera.main.transform.forward * warpMaxDistance;
                validWarpTargetPosition = false;
                warpPointMarker.SetActive(false);
                yield return null;
                continue;
            }

            if(validWarpTargetPosition = Physics.Raycast(sphereCenter, Vector3.down, out castResult, float.MaxValue, warpTargetMask))
            {
                warpPointMarker.transform.position = castResult.point;
                warpPointMarker.SetActive(true);
            }

            //Debug.Log("Marking");
            yield return null;
        }
    }
}