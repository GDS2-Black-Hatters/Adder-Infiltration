using System.Collections;
using UnityEngine;

public class Bomber : Enemy
{
    [Header("Bomber Params"), SerializeField] private float sprintSpeed; //Sprint speed when bombing
    [SerializeField] private float explosionRange = 5;
    [SerializeField] private float explosionDamage = 10;

    [SerializeField] private Animator bomberAnim;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem explosionChargingParticle;

    protected override void DetectionState()
    {
        forwardPower = sprintSpeed;
        base.DetectionState();
    }

    protected override void AttackState()
    {
        FixedStateAction = null;
        bomberAnim.SetBool("isAttacking", true);
        Instantiate(explosionChargingParticle, transform);
        StartCoroutine(SuicideBombing(3));
    }
    
    IEnumerator SuicideBombing(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //TODO: Instantiate Explosion prefab
        Destroy(gameObject);
    }
}
