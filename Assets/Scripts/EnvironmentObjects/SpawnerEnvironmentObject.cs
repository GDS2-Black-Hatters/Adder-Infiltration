using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnvironmentObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPointOffset;
    [SerializeField] private TimeTracker spawnTimer = new(5, -1, true);

    private void Start()
    {
        spawnTimer.onFinish += Spawn;
        spawnTimer.Reset();
    }

    private void Update()
    {
        spawnTimer.Update(Time.deltaTime);
    }

    private void Spawn()
    {
        Instantiate(objectToSpawn, transform.position + spawnPointOffset, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + spawnPointOffset, 0.25f);
    }
}
