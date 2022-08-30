using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float damage = 2;
    [SerializeField] private TimeTracker timer = new(0);

    private void OnEnable()
    {
        timer.Reset();
    }

    void Update()
    {
        timer.Update(Time.deltaTime);
        gameObject.SetActive(timer.tick > 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            //Decrease health through VariableManager.
            GameManager.VariableManager.playerHealth.ReduceHealth(damage);
            timer.Finish();
        }
        timer.Finish();
    }
}
