using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterMatter : WeaponMatter
{
    [SerializeField] private float bulletSpeed = 10;   
    [SerializeField] private float fireCooldown = 0.75f;   

    private TimeTracker fireCooldownTimer;

    [SerializeField] protected AK.Wwise.Event ShootSound;

    private void Awake()
    {
        fireCooldownTimer = new(fireCooldown);
        fireCooldownTimer.Update(Random.Range(0, fireCooldown)); //give the weapon a random initial
    }

    protected override void AttackUpdate()
    {
        if(target == null)
            return;

        fireCooldownTimer.Update(Time.deltaTime);
        if(fireCooldownTimer.tick == 0)
        {
            fireCooldownTimer.SetTimer(fireCooldown * Random.Range(0.97f, 1.03f));
            Rigidbody bullet = GameManager.PoolManager.GetObjectFromPool<Rigidbody>("BulletPool");
            bullet.transform.position = transform.position;

            Bullet bull = bullet.GetComponent<Bullet>();
            bull.SetOwner(GameManager.LevelManager.ActiveSceneController.Player.transform);

            bullet.transform.LookAt(target);
            bullet.velocity = bull.transform.forward * bulletSpeed;

            ShootSound.Post(gameObject);
        }
    }
}
