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

        bool isPlayer = other.CompareTag("Player");
        if (isPlayer || other.gameObject.TryGetComponent(out MeshRenderer _))
        {
            GameManager.VariableManager.playerHealth.ReduceHealth(isPlayer ? damage : 0);
            lifeTime.Reset(true);
        }
    }
}
