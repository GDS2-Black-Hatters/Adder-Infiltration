using UnityEngine;

public class Shark : Enemy
{
    [Header("Shark Params"), SerializeField] private float alwaysChaseSpeed;
    [SerializeField] private GameObject susIcon;

    protected override void NormalState()
    {
        FixedStateAction = SharkPatrol;
    }

    protected override void DetectionState()
    {
        gameObject.SetActive(false);
    }

    protected override void AttackState() {}

    protected void SharkPatrol()
    {
        FixedPatrol();
        susIcon.SetActive(false);
        //Basically FixedChase() but in doing it in Patrol
        Transform player = GameManager.LevelManager.ActiveSceneController.Player.transform;
        Vector3 dir = player.position - transform.position;
        bool hit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, int.MaxValue, raycastMask);
        if (hit && hitInfo.transform == player) 
        {
            PIDTurnTowards(player);
            forwardPower = alwaysChaseSpeed;
            PIDMoveTowards(player);
            susIcon.SetActive(true);
            GameManager.LevelManager.ActiveSceneController.enemyAdmin.IncreaseAlertness(CanAttack ? 1 : 0);
        }
    }
}
