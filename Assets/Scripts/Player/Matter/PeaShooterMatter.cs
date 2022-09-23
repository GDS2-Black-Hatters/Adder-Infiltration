using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterMatter : WeaponMatter
{
    [SerializeField] private float bulletSpeed = 10;   
    [SerializeField] private float fireCooldown = 0.75f;   

    private Transform target; 
    private TimeTracker fireCooldownTimer;

    [SerializeField] protected AK.Wwise.Event ShootSound;

    private void Awake()
    {
        fireCooldownTimer = new(fireCooldown);
        fireCooldownTimer.Update(Random.Range(0, fireCooldown)); //give the weapon a random initial
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        AttackUpdate();
    }

    public override void ChangeTarget(Transform newTargetTransform)
    {
        base.ChangeTarget(newTargetTransform);
        target = newTargetTransform;
    }

    private void AttackUpdate()
    {
        fireCooldownTimer.Update(Time.deltaTime);
        if(fireCooldownTimer.tick == 0)
        {
            fireCooldownTimer.SetTimer(fireCooldown * Random.Range(0.97f, 1.03f));
            Rigidbody bullet = GameManager.PoolManager.GetObjectFromPool<Rigidbody>("BulletPool");
            bullet.transform.position = transform.position;

            Bullet bull = bullet.GetComponent<Bullet>();
            bull.SetOwner(GameManager.LevelManager.player);

            bullet.transform.LookAt(target);
            bullet.velocity = bull.transform.forward * bulletSpeed;

            ShootSound.Post(gameObject);
        }
    }
}
