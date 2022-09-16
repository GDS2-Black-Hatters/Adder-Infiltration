using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGBuildingModule : MonoBehaviour
{
    [field: SerializeField] public Vector3 attatchPoint { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position + attatchPoint, 1f);
    }
}
