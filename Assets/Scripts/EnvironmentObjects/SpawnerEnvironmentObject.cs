using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnvironmentObject : BaseEnvironmentObject
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPointOffset;
    [SerializeField] private TimeTracker spawnTimer = new(5, -1, true);
    [SerializeField] private Animator animCtr;

    private void Start()
    {
        GameManager.LevelManager.ActiveSceneController.onPlayerDetection += ActivateSpawner;
        spawnTimer.Reset();
        spawnTimer.onFinish += Spawn;
        enabled = false;
    }

    private void Update()
    {
        spawnTimer.Update(Time.deltaTime);
    }

    private void ActivateSpawner()
    {
        animCtr.SetBool("IsActive", enabled = true);
    }

    private void Spawn()
    {
        Instantiate(
            objectToSpawn, transform.position + spawnPointOffset, Quaternion.identity,
            GameManager.LevelManager.ActiveSceneController.enemyAdmin.EnemiesParent
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + spawnPointOffset, 0.25f);
    }
}
