using UnityEngine;

public class Shooter : Enemy
{
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed = 3;
    [SerializeField] protected TimeTracker attackCooldown = new(1); //Intervals before next attack.

    protected override void Attack()
    {
        attackCooldown.Update(Time.deltaTime);
        if (attackCooldown.tick == 0)
        {
            attackCooldown.Reset();
            Rigidbody bullet = GameManager.PoolManager.GetObjectFromPool<Rigidbody>("BulletPool");
            bullet.transform.position = bulletPoint ? bulletPoint.position : Vector3.zero;
            bullet.transform.LookAt(GameManager.LevelManager.player);
            bullet.velocity = (GameManager.LevelManager.player.position - bullet.transform.position) * bulletSpeed;
        }
    }
}
