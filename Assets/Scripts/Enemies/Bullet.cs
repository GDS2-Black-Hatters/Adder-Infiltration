using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    [SerializeField] private TimeTracker lifeTime = new(0);
    private Transform owner;

    private void OnEnable()
    {
        lifeTime.Reset();
    }

    void Update()
    {
        lifeTime.Update(Time.deltaTime);
        gameObject.SetActive(lifeTime.tick > 0);
    }

    public void SetOwner(Transform transform)
    {
        owner = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet") || other.transform.IsChildOf(owner))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            //Decrease health through VariableManager.
            GameManager.VariableManager.playerHealth.ReduceHealth(damage);
            lifeTime.Reset(true);
        }

        if (other.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            lifeTime.Reset(true);
        }
    }
}
