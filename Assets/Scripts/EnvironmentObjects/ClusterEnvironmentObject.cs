using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterEnvironmentObject : BaseEnvironmentObject
{
    [SerializeField] private BaseEnvironmentObject[] availableChildEnvObjects;
    [SerializeField] private Vector3[] childObjOffset;

    public override void Initilize()
    {
        if(availableChildEnvObjects.Length <= 0)
        {
            Debug.LogWarning("No child object available for cluster environment object spawn");
        }

        foreach(Vector3 offset in childObjOffset)
        {
            Instantiate(availableChildEnvObjects[Random.Range(0, availableChildEnvObjects.Length)], transform.position + offset, Quaternion.identity, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach(Vector3 offset in childObjOffset)
        {
            Gizmos.DrawWireSphere(transform.position + offset, 0.2f);
        }
    }
}
