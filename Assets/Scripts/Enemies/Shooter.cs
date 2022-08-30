using UnityEngine;

/// <summary>
/// Basic Shooter AI, inherits from Enemy.
/// </summary>
public class Shooter : Enemy
{
    [SerializeField] private Transform bulletPoint; //Where the bullet will spawn.
    [SerializeField] private float bulletSpeed = 3; //The speed of the bullet.
    [SerializeField] protected TimeTracker attackCooldown = new(1); //Intervals before next attack.

    protected override void Attack()
    {
        attackCooldown.Update(Time.deltaTime);
        if (attackCooldown.tick == 0)
        {
            attackCooldown.Reset();
            Rigidbody bullet = GameManager.PoolManager.GetObjectFromPool<Rigidbody>("BulletPool");
            bullet.transform.position = bulletPoint ? bulletPoint.position : Vector3.zero;

            Transform player = GameManager.LevelManager.player;
            bullet.transform.LookAt(player);
            bullet.velocity = (player.position - bullet.transform.position) * bulletSpeed;
        }
    }
}
