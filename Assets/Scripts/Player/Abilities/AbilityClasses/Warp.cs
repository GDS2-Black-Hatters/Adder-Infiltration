using System.Collections;
using UnityEngine;
using static VariableManager;

public class Warp : AbilityBase
{
    [SerializeField] private GameObject warpPointMarker;
    [SerializeField] private float warpMaxDistance = 30;
    [SerializeField] private LayerMask warpTargetMask;
    private const float warpRadius = 1.6f;

    private Coroutine warpPointMarkCoroutine;
    private bool validWarpTargetPosition;

    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.Warp;

    protected void Start()
    {
        warpPointMarker.SetActive(false);
    }

    protected override void DoAbilityEffect()
    {
        if (!validWarpTargetPosition)
        {
            return;
        }

        GameManager.LevelManager.player.transform.position = warpPointMarker.transform.position + Vector3.up * GameManager.LevelManager.player.transform.position.y;
        EndAbilityPrime();
    }

    public override void EndAbilityPrime()
    {
        if (warpPointMarkCoroutine != null)
        {
            StopCoroutine(warpPointMarkCoroutine);
        }

        validWarpTargetPosition = false;
        warpPointMarker.SetActive(false);
    }

    public override void StartAbilityPrime()
    {
        if (warpPointMarkCoroutine != null)
        {
            StopCoroutine(warpPointMarkCoroutine);
        }

        warpPointMarker.SetActive(true);

        warpPointMarkCoroutine = StartCoroutine(updateWarpPointMark());
    }

    private IEnumerator updateWarpPointMark()
    {
        Vector3 sphereCenter;
        while (true)
        {
            if (Physics.SphereCast(Camera.main.transform.position, warpRadius, Camera.main.transform.forward, out RaycastHit castResult, warpMaxDistance, warpTargetMask))
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

            if (validWarpTargetPosition = Physics.Raycast(sphereCenter, Vector3.down, out castResult, float.MaxValue, warpTargetMask))
            {
                warpPointMarker.transform.position = castResult.point;
                warpPointMarker.SetActive(true);
            }

            //Debug.Log("Marking");
            yield return null;
        }
    }
}
