using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnvironmentObject : BaseEnvironmentObject
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPointOffset;
    [SerializeField] private TimeTracker spawnTimer = new(5, -1, true);

    [SerializeField] private Animator animCtr;

    private bool isActive;

    private void Start()
    {
        GameManager.LevelManager.ActiveSceneController.onPlayerDetection += ActivateSpawner;
        spawnTimer.Reset();
        spawnTimer.onFinish += Spawn;
    }

    private void Update()
    {
        if(isActive)
        {
            spawnTimer.Update(Time.deltaTime);
        }
    }

    private void ActivateSpawner()
    {
        isActive = true;
        animCtr.SetBool("IsActive", true);
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
