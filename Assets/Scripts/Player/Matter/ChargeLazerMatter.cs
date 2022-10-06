using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLazerMatter : WeaponMatter
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float chargeRate = 1;

    private float chargeProgress = 0;

    protected override void AttackUpdate()
    {
        float chargeDelta = (target != null) ? Time.deltaTime : -Time.deltaTime;
        chargeDelta *= chargeRate;

        chargeProgress = Mathf.Max(chargeProgress + chargeDelta, 0);

        if(chargeProgress >= 1)
        {
            chargeProgress -= 1;
            
            //Spawn Lazer
            Lazer lazer = GameManager.PoolManager.GetObjectFromPool<Lazer>("LazerPool");
            lazer.transform.position = transform.position;
            lazer.InitilizeLazer(target.position - transform.position);

            //do damage
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
