using UnityEngine;

/// <summary>
/// Basic Shooter AI, inherits from Enemy.
/// </summary>
public class Shooter : Enemy
{
    [SerializeField] private Transform bulletPoint; //Where the bullet will spawn.
    [SerializeField] private float bulletSpeed = 10; //The speed of the bullet.
    [SerializeField] protected TimeTracker attackCooldown = new(1, -1, true); //Intervals before next attack.

    private void Awake()
    {
        attackCooldown.Reset();
        attackCooldown.onFinish += Shoot;
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
