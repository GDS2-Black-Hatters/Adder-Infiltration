using System.Collections;
using UnityEngine;

public class Bomber : Enemy
{
    [Header("Bomber Params"), SerializeField] private float sprintSpeed; //Sprint speed when bombing

    [SerializeField] private float explosionRange = 5;

    [SerializeField] private float explosionDamage = 10;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem explosionChargingParticle;
    private bool charging = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        Debug.Log(gameObject+"Start Suicide!!!!!!!!");
        forwardPower = 0; //Stand still while charging
        if(!charging)
        {
            //Spawn charging particle
            Instantiate(explosionChargingParticle, transform.position, Quaternion.identity);
        }
        charging = true;
        DoSuicideBombing(3); 
    }

    protected virtual void Bombing()
    {
        Debug.Log(gameObject+"ALLAHU AKBAR!");

        //Within explosion range
        if ((GameManager.LevelManager.player.position - transform.position).sqrMagnitude <= explosionRange) 
        {
            GameManager.VariableManager.playerHealth.ReduceHealth(explosionDamage);
            
            //Add explosion particle fx
            Instantiate(explosionParticle, transform.position, Quaternion.identity);

            //Destroy self
            Destroy(gameObject);
        }
    }

    private void DoSuicideBombing(float delayTime)
    {

        StartCoroutine(SuicideBombing(delayTime));
    }
    
    IEnumerator SuicideBombing(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
                
        //Do the action after the delay time has finished.
        forwardPower = sprintSpeed; //Increase Movement Speed to Sprint Speed
        stateAction = Bombing;
        fixedStateAction = FixedChase;
        movementPIDController.derivativeGain = 0;
    }
}
