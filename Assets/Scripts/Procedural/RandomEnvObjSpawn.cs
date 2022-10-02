using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnvObjSpawn : MonoBehaviour
{
    [SerializeField] private BaseEnvironmentObject[] availableObjectToSpawn;
    [SerializeField] public Vector3 spawnPoint;

    private void Start()
    {
        BaseEnvironmentObject newObj = Instantiate(availableObjectToSpawn[Random.Range(0, availableObjectToSpawn.Length - 1)]);
        newObj.transform.SetParent(transform);
        newObj.transform.localPosition = spawnPoint;
        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position + spawnPoint, 1f);
    }
}
