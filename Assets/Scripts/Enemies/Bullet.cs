using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
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
        if (other.transform.IsChildOf(owner) || !other.gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            //Decrease health through VariableManager.
        }
        timer.Finish();
    }
}
