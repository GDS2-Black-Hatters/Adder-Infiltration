using UnityEngine;

public class Shark : Enemy
{
    private float startSpeed;
    [Header("Shark Params"), SerializeField] private float alwaysChaseSpeed;
    [SerializeField] private float distanceDetection = 15;
    [SerializeField] private GameObject susIcon;
    private bool wasChasing = false;

    protected override void Awake()
    {
        base.Awake();
        startSpeed = forwardPower;
    }

    protected override void NormalState()
    {
        FixedStateAction = SharkPatrol;
    }

    protected override void DetectionState()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    protected override void AttackState() {}

    protected void SharkPatrol()
    {
        //Basically FixedChase() but in doing it in Patrol
        Transform player = GameManager.LevelManager.ActiveSceneController.Player.transform;
        Vector3 dir = player.position - transform.position;
        bool hit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, distanceDetection, raycastMask);
        bool isPlayer = hit && hitInfo.transform == player;

        susIcon.SetActive(isPlayer);
        if (isPlayer) 
        {
            wasChasing = true;
            forwardPower = alwaysChaseSpeed;
            PIDTurnTowards(player);
            PIDMoveTowards(player);
            return;
        }
        forwardPower = startSpeed;
        if (wasChasing)
        {
            wasChasing = false;
            nodeTarget = GameManager.LevelManager.ActiveSceneController.enemyAdmin.GetClosestNode(transform);
        }
        FixedPatrol();
    }
}
