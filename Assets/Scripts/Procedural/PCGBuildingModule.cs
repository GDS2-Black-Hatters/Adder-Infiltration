using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGBuildingModule : MonoBehaviour
{
    [field: SerializeField] public Vector3 attatchPoint { get; private set; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.TransformPoint(attatchPoint), 1f);
    }
}
