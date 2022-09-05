using UnityEngine;

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
    public GameObject nodeParent;
    
    public float speed = 10.0f;
    private Vector3 patrolDirection;
    private Quaternion lookRotation;
    private void Awake()
    {
        attackCooldown.Reset();
        attackCooldown.onFinish += Shoot;
    }

    protected override void Patrol() 
    {
        if (currentNode <= nodeParent.GetComponent<NodeParent>().nodes.Length - 2)
        {
            nextNode = currentNode + 1;
        }
        else
        {
            nextNode = 1;
        }

        patrolDirection = (nodeParent.GetComponent<NodeParent>().nodes[nextNode].transform.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(patrolDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1000);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if (Vector3.Distance(transform.position, nodeParent.GetComponent<NodeParent>().nodes[nextNode].transform.position) <= patrolNodeDistanceLeeway && currentNode != nextNode)
        {
            currentNode = nextNode;
        }
        Debug.Log(gameObject.name + " " + nextNode);
    }
        
    protected override void Attack()
    {
        attackCooldown.Update(Time.deltaTime);
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
