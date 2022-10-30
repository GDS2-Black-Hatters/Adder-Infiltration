using UnityEngine;

/// <summary>
/// Basic Shooter AI, inherits from Enemy.
/// </summary>
[RequireComponent(typeof(HoverAtHeight))]
public class Shooter : Enemy
{
    [Header("Shooter Params"), SerializeField] private Transform bulletPoint; //Where the bullet will spawn.
    [SerializeField] private float bulletSpeed = 10; //The speed of the bullet.
    [SerializeField] protected TimeTracker attackCooldown = new(1); //Intervals before next attack.
    
    [SerializeField] protected AK.Wwise.Event ShootSound;
    private HoverAtHeight hoverAt;

    protected override void Awake()
    {
        base.Awake();
        attackCooldown.Reset();
        attackCooldown.onFinish += Shoot;
        hoverAt = GetComponent<HoverAtHeight>();
    }

    protected override void StunStart()
    {
        base.StunStart();
        hoverAt.enabled = false;
    }

    protected override void StunEnd()
    {
        base.StunEnd();
        hoverAt.enabled = true;
    }

    protected override void AttackState()
    {
        StateAction = Attack;
        FixedStateAction = null;
    }

    protected void Attack()
    {
        attackCooldown.Update(Time.deltaTime);
        if (!CanAttack)
        {
            StateAction = null;
            FixedStateAction = FixedChase;
        }
    }

    private void Shoot()
    {
        Rigidbody bullet = GameManager.PoolManager.GetObjectFromPool<Rigidbody>("BulletPool");
        bullet.transform.position = bulletPoint ? bulletPoint.position : Vector3.zero;

        Bullet bull = bullet.GetComponent<Bullet>();
        bull.SetOwner(transform);

        Transform player = GameManager.LevelManager.ActiveSceneController.Player.transform;
        bullet.transform.LookAt(player);
        bullet.velocity = (player.position - bullet.transform.position).normalized * bulletSpeed;

        ShootSound.Post(gameObject);
        attackCooldown.Reset();
    }
}
