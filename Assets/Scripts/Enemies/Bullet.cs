using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float damage = 2;
    [SerializeField] private TimeTracker timer = new(0);
    private Transform owner;

    private void OnEnable()
    {
        timer.Reset();
    }

    void Update()
    {
        timer.Update(Time.deltaTime);
        gameObject.SetActive(timer.tick > 0);
    }

    public void SetOwner(Transform transform)
    {
        owner = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Decrease health through VariableManager.
            GameManager.VariableManager.playerHealth.ReduceHealth(damage);
            timer.Finish();
        }

        if (other.transform.IsChildOf(owner) || !other.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            return;
        }
        timer.Finish();
    }
}
