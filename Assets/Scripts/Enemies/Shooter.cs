using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Basic Shooter AI, inherits from Enemy.
/// </summary>
public class Shooter : Enemy
{
    private const float patrolNodeDistanceLeeway = 0.2f;

    [SerializeField] private Transform bulletPoint; //Where the bullet will spawn.
    [SerializeField] private float bulletSpeed = 10; //The speed of the bullet.
    [SerializeField] protected TimeTracker attackCooldown = new(1, -1, true); //Intervals before next attack.
    public int currentNode;
    public int nextNode;

    //QUICK PATCHED, CHANGE SOON
    [SerializeField] private NodeParent nodeParent;
    
    private Rigidbody rb;
    private Vector3 patrolDirection;
    private Quaternion lookRotation;
    [SerializeField] private float closeRangeDistance = 10;

    private void Awake()
    {
        attackCooldown.Reset();
        attackCooldown.onFinish += Shoot;

        rb = GetComponent<Rigidbody>();
    }

    public void SetNodeParent(NodeParent nodeParent)
    {
        this.nodeParent = nodeParent;
    }

    protected override void Patrol() 
    {
        if (currentNode <= nodeParent.nodes.Length - 2)
        {
            nextNode = currentNode + 1;
        }
        else
        {
            nextNode = 1;
        }

        GoTowards(nodeParent.nodes[nextNode].transform);
        if (Vector3.Distance(transform.position, nodeParent.nodes[nextNode].transform.position) <= patrolNodeDistanceLeeway && currentNode != nextNode)
        {
            currentNode = nextNode;
        }
    }
        
    protected override void Attack()
    {
        Chase();
        attackCooldown.Update(Time.deltaTime);
    }

    protected override void Chase()
    {
        Transform player = GameManager.LevelManager.player;
        if (Vector3.Distance(transform.position, player.transform.position) > closeRangeDistance)
        {
            GoTowards(player);
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void GoTowards(Transform target)
    {
        transform.LookAt(target);
        Vector3 rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        rb.velocity = (target.position - transform.position).normalized * speed;
    }

    private void Shoot()
    {
        Rigidbody bullet = GameManager.PoolManager.GetObjectFromPool<Rigidbody>("BulletPool");
        bullet.transform.position = bulletPoint ? bulletPoint.position : Vector3.zero;

        Bullet bull = bullet.GetComponent<Bullet>();
        bull.SetOwner(transform);

        Transform player = GameManager.LevelManager.player;
        bullet.transform.LookAt(player);
        bullet.velocity = (player.position - bullet.transform.position).normalized * bulletSpeed;
    }
}
