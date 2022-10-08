using UnityEngine;

public class Slower : Enemy
{
    [Header("Slower Params"), SerializeField] private GameObject Obstacles;
    //[SerializeField] private Animator slowerAnim;
    [SerializeField] private ParticleSystem constructingParticle;
    [SerializeField] protected TimeTracker attackCooldown = new(5, -1, true); //Intervals before next attack.
    protected override void Awake()
    {
        base.Awake();
        attackCooldown.Reset();
        attackCooldown.onFinish += Construct;

        rb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        attackCooldown.Update(Time.deltaTime);
        if ((GameManager.LevelManager.player.transform.position - transform.position).sqrMagnitude > closeRangeDistance)
        {
            stateAction = Chase;
            fixedStateAction = FixedChase;
        }
    }

    private void Construct()
    {
        //slowerAnim.SetBool("isConstructing", true);
        Debug.Log(gameObject + "Is Constructing!!!");
        Instantiate(constructingParticle, transform.position, Quaternion.identity);
        Instantiate(Obstacles, GameManager.LevelManager.player.transform.position, Quaternion.identity);
    }
}
