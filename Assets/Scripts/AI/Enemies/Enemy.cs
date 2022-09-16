using System;
using UnityEngine;

/// <summary>
/// Basic abstract class of the enemy script behaviour.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [HideInInspector] public bool canAttack = false;
    private int raycastMask;
    protected Rigidbody rb;

    [Header("Stats"), SerializeField] protected float speed;
    [SerializeField] protected Health health;
    [SerializeField] protected float closeRangeDistance = 10;

    [Header("AI Pathfinding"),Tooltip("Custom Patrol Path for the AI Enemy to follow. Leave empty for random movement."), SerializeField]
    private AINode[] customPatrolPath;
    private int customIndex = 0;

    protected AINode nodeTarget;
    [SerializeField] protected float nodeLeniency = 0.5f;
    protected Action stateAction; //You should only be assigning not subscribing.

    protected virtual void Awake()
    {
        health.Reset();
        raycastMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Terrain"); //Bruh, bit shifting be wild.
    }

    protected virtual void Start()
    {
        BaseSceneController controller = GameManager.LevelManager.ActiveSceneController;
        controller.onPlayerDetection += OnPlayerDetection;
        stateAction = controller.sceneMode == BaseSceneController.SceneState.Stealth ? Patrol : Chase;
        nodeTarget = customPatrolPath.Length == 0 ? controller.GetClosestNode(transform) : customPatrolPath[0];
    }

    protected void Update()
    {
        stateAction?.Invoke();
    }

    protected virtual void OnPlayerDetection()
    {
        stateAction = Chase;
        nodeTarget = GameManager.LevelManager.ActiveSceneController.GetClosestNode(transform).GetNextNodeToPlayer();
    }

    protected void GoTowards(Transform target)
    {
        rb.velocity = target ? (target.position - transform.position).normalized * speed : Vector3.zero;
    }

    protected void LookAt(Transform target)
    {
        transform.LookAt(target);
        Vector3 rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;
    }

    protected virtual void Patrol()
    {
        if (!nodeTarget)
        {
            return;
        }
        
        LookAt(nodeTarget.transform);
        GoTowards(nodeTarget.transform);
        if ((nodeTarget.transform.position - transform.position).sqrMagnitude < nodeLeniency)
        {
            nodeTarget = customPatrolPath.Length < 2 ? nodeTarget.GetRandomNeighbour() : customPatrolPath[customIndex = (customIndex + 1) % customPatrolPath.Length];
        }
    }

    protected virtual void Chase()
    {
        Transform player = GameManager.LevelManager.player;
        Vector3 dir = player.position - transform.position;
        bool hit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, int.MaxValue, raycastMask);
        if (hit && hitInfo.transform == player) //If the enemy can see the player, book it to them.
        {
            nodeTarget = null;
            LookAt(player);
            GoTowards(dir.sqrMagnitude > closeRangeDistance ? player : null);
            if (dir.sqrMagnitude < closeRangeDistance)
            {
                stateAction = Attack;
            }
        } else
        {
            if (!nodeTarget || (nodeTarget.transform.position - transform.position).sqrMagnitude < nodeLeniency)
            {
                nodeTarget = GameManager.LevelManager.ActiveSceneController.GetClosestNode(transform).GetNextNodeToPlayer();
            }
            LookAt(nodeTarget.transform);
            GoTowards(nodeTarget.transform);
        }
    }

    protected abstract void Attack();
}
