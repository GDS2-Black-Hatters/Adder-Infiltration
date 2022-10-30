using System.Collections;
using UnityEngine;

public class Bomber : Enemy
{
    [Header("Bomber Params"), SerializeField] private float sprintSpeed; //Sprint speed when bombing
    [SerializeField] private Animator bomberAnim;
    [SerializeField] private ParticleSystem explosionChargingParticle;

    [SerializeField] protected AK.Wwise.Event chargingSFXEvent;
    [SerializeField] protected AK.Wwise.Event explosionSFXEvent;

    protected override void DetectionState()
    {
        forwardPower = sprintSpeed;
        base.DetectionState();
    }

    protected override void StunStart()
    {
        base.StunStart();
        bomberAnim.enabled = false;
    }

    protected override void StunEnd()
    {
        base.StunEnd();
        bomberAnim.enabled = true;
    }

    protected override void AttackState()
    {
        FixedStateAction = null;
        bomberAnim.SetBool("isAttacking", true);
        explosionChargingParticle.gameObject.SetActive(true);
        chargingSFXEvent.Post(gameObject);
        StartCoroutine(SuicideBombing(3));
    }
    
    IEnumerator SuicideBombing(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        explosionSFXEvent.Post(gameObject);
        GameManager.PoolManager.GetObjectFromPool<Transform>("ExplosionParticlePool").position = transform.position;
        Destroy(gameObject);
    }
}
