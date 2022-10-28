using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

/// <summary>
/// Basic abstract class of the enemy script behaviour.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [HideInInspector] public bool CanAttack { get; protected set; } = false;
    protected int raycastMask;
    protected Rigidbody rb;
    protected EnemyAttackRange attackRange;
    protected EnemyDetectionRange detectionRange;
    private bool isStunned = false;

    [Header("Stats"), SerializeField] protected Health health;

    [Header("Movement Behaviour")]
    [SerializeField] protected PIDController movementPIDController = new();
    [SerializeField] protected float forwardPower = 8;
    [SerializeField] protected PIDController rotationPIDController = new();
    [SerializeField] protected float torqueForce = 30;

    [Header("AI Pathfinding"), Tooltip("Custom Patrol Path for the AI Enemy to follow. Leave empty for random movement."), SerializeField]
    private List<AINode> customPatrolPath;
    private int customIndex = 0;

    protected AINode nodeTarget;
    [SerializeField] private float nodeLeniency = 0.5f;

    protected Action StateAction; //You should only be assigning not subscribing.
    protected Action FixedStateAction;

    protected virtual void Awake()
    {
        health.Reset();
        health.onDeath += Death;
        raycastMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Terrain") | 1 << LayerMask.NameToLayer("InvisibleWall");
        rb = GetComponent<Rigidbody>();
        attackRange = GetComponentInChildren<EnemyAttackRange>();
        detectionRange = GetComponentInChildren<EnemyDetectionRange>();
    }

    protected virtual void Start()
    {
        BaseSceneController controller = GameManager.LevelManager.ActiveSceneController;
        controller.enemyAdmin.OnFullAlert += DetectionState;
        GetComponentInChildren<EnemyAttackRange>().AddTrigger((attack) => { CanAttack = attack; });

        if (!controller.enemyAdmin.FullAlertTriggered)
        {
            NormalState();
        }
        else
        {
            DetectionState();
        }
        MovementStart();
    }

    protected void Update()
    {
        if (isStunned)
        {
            StunUpdate();
            return;
        }
        StateAction?.Invoke();
        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        GameManager.LevelManager.ActiveSceneController.enemyAdmin.OnFullAlert -= DetectionState;
    }

    protected void FixedUpdate()
    {
        if (isStunned)
        {
            FixedStunUpdate();
            return;
        }
        FixedStateAction?.Invoke();
    }

    protected virtual void NormalState()
    {
        FixedStateAction = FixedPatrol;
    }

    protected virtual void DetectionState()
    {
        FixedStateAction = FixedChase;
    }

    protected virtual void StunUpdate() { }
    protected virtual void FixedStunUpdate() { }
    
    protected virtual void StunStart()
    {
        attackRange.gameObject.SetActive(false);
        detectionRange.gameObject.SetActive(false);
    }

    protected virtual void StunEnd()
    {
        MovementStart();
        attackRange.gameObject.SetActive(true);
        detectionRange.gameObject.SetActive(true);
    }

    protected abstract void AttackState();

    public virtual void TakeDamage(float amount)
    {
        health.ReduceHealth(amount);
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void StartStun(float seconds)
    {
        StartCoroutine(Stun(seconds));
    }

    private IEnumerator Stun(float duration)
    {
        if (!isStunned)
        {
            StunStart();
            isStunned = true;
            TimeTracker time = new(duration, 1);
            time.onFinish += () =>
            {
                StunEnd();
                isStunned = false;
            };

            do
            {
                yield return null;
                time.Update(Time.deltaTime);
            } while (isStunned);
        }
    }

    #region Basic Movement
    private void MovementStart()
    {
        nodeTarget = customPatrolPath.Count == 0 ? GameManager.LevelManager.ActiveSceneController.enemyAdmin.GetClosestNode(transform) : customPatrolPath[customIndex];
    }

    protected void PIDMoveTowards(Transform target)
    {
        float throttle = movementPIDController.Update(Time.fixedDeltaTime, -Vector3.Distance(transform.position, target.position), 0);
        rb.AddForce(MathF.Max(throttle, -0.1f) * forwardPower * Vector3.Dot(transform.forward, Vector3.Scale(target.position - transform.position, new Vector3(1, 0, 1)).normalized) * transform.forward);
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

    /// <summary>
    /// Basic Fixed Patrol Behaviour.
    /// </summary>
    protected void FixedPatrol()
    {
        if (!nodeTarget)
        {
            return;
        }

        PIDTurnTowards(nodeTarget.transform);
        PIDMoveTowards(nodeTarget.transform);
        if ((nodeTarget.transform.position - transform.position).sqrMagnitude < nodeLeniency)
        {
            nodeTarget = customPatrolPath.Count < 2 ? nodeTarget.GetRandomNeighbour() : DoStatic.GetElement(++customIndex, ref customPatrolPath);
        }
    }

    /// <summary>
    /// Basic Fixed Chase Behaviour.
    /// </summary>
    /// <returns>True if the enemy is within attack range.</returns>
    protected void FixedChase()
    {
        Transform player = GameManager.LevelManager.ActiveSceneController.Player.transform;
        Vector3 dir = player.position - transform.position;
        bool hit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, float.MaxValue, raycastMask);
        if (hit && hitInfo.transform == player) //If the enemy can see the player, book it to them.
        {
            nodeTarget = null;
            PIDTurnTowards(player);
            PIDMoveTowards(player);
            if (CanAttack)
            {
                AttackState();
            }
            return;
        }

        if (!nodeTarget || (nodeTarget.transform.position - transform.position).sqrMagnitude < nodeLeniency)
        {
            nodeTarget = GameManager.LevelManager.ActiveSceneController.enemyAdmin.GetClosestNode(transform).GetNextNodeToPlayer();
        }
        PIDTurnTowards(nodeTarget.transform);
        PIDMoveTowards(nodeTarget.transform);
    }
    #endregion
}
