using UnityEngine;

public class Slower : Enemy
{
    [Header("Slower Params"), SerializeField] private GameObject Obstacles;
    [SerializeField] private Animator slowerAnim;
    [SerializeField] private ParticleSystem constructingParticle;
    [SerializeField] protected TimeTracker attackCooldown = new(5); //Intervals before next attack.

    protected override void Awake()
    {
        base.Awake();
        attackCooldown.Reset();
        attackCooldown.onFinish += Construct;
    }

    protected override void StunStart()
    {
        base.StunStart();
        slowerAnim.enabled = false;
    }

    protected override void StunEnd()
    {
        base.StunEnd();
        slowerAnim.enabled = true;
    }

    protected override void AttackState()
    {
        StateAction = () =>
        {
            attackCooldown.Update(Time.deltaTime);
            if (!CanAttack)
            {
                slowerAnim.SetBool("isAttacking", false);
                StateAction = null;
                FixedStateAction = FixedChase;
            }
        };
        FixedStateAction = null;
    }

    private void Construct()
    {
        slowerAnim.SetBool("isAttacking", true);
        Instantiate(constructingParticle, transform.position, Quaternion.identity);
        Instantiate(Obstacles, GameManager.LevelManager.ActiveSceneController.Player.transform.position, Quaternion.identity);
        attackCooldown.Reset();
    }
}
