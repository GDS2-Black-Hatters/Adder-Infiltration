using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEnvObject : BaseEnvironmentObject
{
    [field: SerializeField] public float objectLength { get; private set; }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new(1, 0.1f, objectLength));
    }
}
