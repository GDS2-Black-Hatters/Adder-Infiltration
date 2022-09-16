using UnityEngine;

/// <summary>
/// Basic Shooter AI, inherits from Enemy.
/// </summary>
public class Shooter : Enemy
{
    [Header("Shooter Params"), SerializeField] private Transform bulletPoint; //Where the bullet will spawn.
    [SerializeField] private float bulletSpeed = 10; //The speed of the bullet.
    [SerializeField] protected TimeTracker attackCooldown = new(1, -1, true); //Intervals before next attack.

    protected override void Awake()
    {
        base.Awake();
        attackCooldown.Reset();
        attackCooldown.onFinish += Shoot;

        rb = GetComponent<Rigidbody>();
    }

    protected override void Attack()
    {
        attackCooldown.Update(Time.deltaTime);
        if ((GameManager.LevelManager.player.position - transform.position).sqrMagnitude > closeRangeDistance)
        {
            stateAction = Chase;
        }
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
