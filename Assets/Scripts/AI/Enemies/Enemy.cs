using System;
using UnityEngine;

/// <summary>
/// Basic abstract class of the enemy script behaviour.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [HideInInspector] public bool canAttack = false;
    protected int raycastMask;
    protected Rigidbody rb;

    [Header("Stats"), SerializeField] protected float movementSpeed = 10;
    [SerializeField] protected float rotationSpeed = 30;
    [SerializeField] protected Health health;
    [SerializeField] protected float closeRangeDistance = 10;

    [Header("Movement Behaviour")]
    [SerializeField] protected PIDController movementPIDController = new();
    [SerializeField] protected float forwardPower = 8;
    [SerializeField] protected PIDController rotationPIDController = new();
    [SerializeField] protected float torqueForce = 30;

    [Header("AI Pathfinding"),Tooltip("Custom Patrol Path for the AI Enemy to follow. Leave empty for random movement."), SerializeField]
    private AINode[] customPatrolPath;
    private int customIndex = 0;

    protected AINode nodeTarget;
    [SerializeField] protected float nodeLeniency = 0.5f;
    protected Action stateAction; //You should only be assigning not subscribing.
    protected Action fixedStateAction;

    protected virtual void Awake()
    {
        health.Reset();
        health.onDeath += Death;
        raycastMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Terrain"); //Bruh, bit shifting be wild.
    }

    protected virtual void Start()
    {
        BaseSceneController controller = GameManager.LevelManager.ActiveSceneController;
        controller.enemyAdmin.onFullAlert += OnPlayerDetection;
        stateAction = controller.sceneMode == BaseSceneController.SceneState.Stealth ? Patrol : Chase;
        fixedStateAction = controller.sceneMode == BaseSceneController.SceneState.Stealth ? FixedPatrol : FixedChase;
        nodeTarget = customPatrolPath.Length == 0 ? controller.enemyAdmin.GetClosestNode(transform) : customPatrolPath[0];
    }

    protected void Update()
    {
        stateAction?.Invoke();
    }

    protected void FixedUpdate()
    {
        fixedStateAction?.Invoke();
    }

    protected virtual void OnPlayerDetection()
    {
        stateAction = Chase;
        fixedStateAction = FixedChase;
        nodeTarget = GameManager.LevelManager.ActiveSceneController.enemyAdmin.GetClosestNode(transform).GetNextNodeToPlayer();
    }

    protected void PIDMoveTowards(Transform target)
    {
        float throttle = movementPIDController.Update(Time.fixedDeltaTime, -Vector3.Distance(transform.position, target.position), 0);
        rb.AddForce(throttle * forwardPower * Vector3.Dot(transform.forward, (target.position - transform.position).normalized) * transform.forward);
    }

    protected void GoTowards(Transform target)
    {
        rb.velocity = target ? (target.position - transform.position).normalized * movementSpeed : Vector3.zero;
    }

    protected void PIDTurnTowards(Transform target)
    {
        var targetPosition = target.position;
        targetPosition.y = rb.position.y;    //ignore difference in Y
        var targetDir = (targetPosition - rb.position).normalized;
        var forwardDir = rb.rotation * Vector3.forward;

        var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
        var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

        float input = rotationPIDController.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
        rb.AddTorque(new Vector3(0, input * torqueForce, 0));
    }

    protected void LookAt(Transform target)
    {
        transform.LookAt(target);
        Vector3 rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;
    }

    protected virtual void Patrol() {}
    protected virtual void FixedPatrol()
    {
        if (!nodeTarget)
        {
            return;
        }
        
        PIDTurnTowards(nodeTarget.transform);
        PIDMoveTowards(nodeTarget.transform);
        if ((nodeTarget.transform.position - transform.position).sqrMagnitude < nodeLeniency)
        {
            nodeTarget = customPatrolPath.Length < 2 ? nodeTarget.GetRandomNeighbour() : customPatrolPath[customIndex = (customIndex + 1) % customPatrolPath.Length];
        }
    }

    protected virtual void Chase() {}
    protected virtual void FixedChase()
    {
        Transform player = GameManager.LevelManager.ActiveSceneController.Player.transform;
        Vector3 dir = player.position - transform.position;
        bool hit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, int.MaxValue, raycastMask);
        if (hit && hitInfo.transform == player) //If the enemy can see the player, book it to them.
        {
            nodeTarget = null;
            PIDTurnTowards(player);
            if(dir.sqrMagnitude > closeRangeDistance)
            {
                PIDMoveTowards(player);
            }
            //GoTowards(dir.sqrMagnitude > closeRangeDistance ? player : null);
            if (dir.sqrMagnitude < closeRangeDistance)
            {
                stateAction = Attack;
                fixedStateAction = FixedAttack;
            }
        } else
        {
            if (!nodeTarget || (nodeTarget.transform.position - transform.position).sqrMagnitude < nodeLeniency)
            {
                nodeTarget = GameManager.LevelManager.ActiveSceneController.enemyAdmin.GetClosestNode(transform).GetNextNodeToPlayer();
            }
            PIDTurnTowards(nodeTarget.transform);
            PIDMoveTowards(nodeTarget.transform);
        }
    }

    public virtual void TakeDamage(float amount)
    {
        health.ReduceHealth(amount);
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    protected virtual void Attack() { }
    protected virtual void FixedAttack() { }
}
