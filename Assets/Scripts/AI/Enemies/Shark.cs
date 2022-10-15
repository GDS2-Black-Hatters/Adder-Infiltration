using UnityEngine;

public class Shark : Enemy
{
    [Header("Shark Params"), SerializeField] private float alwaysChaseSpeed;
    [SerializeField] private GameObject susIcon;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }

        protected override void FixedPatrol()
    {
        base.FixedPatrol();
        susIcon.SetActive(false);
        //Basically FixedChase() but in doing it in Patrol
        Transform player = GameManager.LevelManager.player.transform;
        Vector3 dir = player.position - transform.position;
        bool hit = Physics.Raycast(transform.position, dir, out RaycastHit hitInfo, int.MaxValue, this.raycastMask);
        if (hit && hitInfo.transform == player) 
        {
            nodeTarget = null;
            PIDTurnTowards(player);
            forwardPower = alwaysChaseSpeed;
            PIDMoveTowards(player);
            susIcon.SetActive(true);
            if (dir.sqrMagnitude < closeRangeDistance)
            {
                GameManager.LevelManager.ActiveSceneController.enemyAdmin.IncreaseAlertness(1f);

                //Destroy all sharks
                GameObject[] sharks = GameObject.FindGameObjectsWithTag("Shark");
                foreach(GameObject shark in sharks)
                {
                    GameObject.Destroy(shark);
                }
            }
        }
    }
    protected override void OnPlayerDetection()
    {
    }
}
