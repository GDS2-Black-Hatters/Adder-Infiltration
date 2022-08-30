using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;
    private TimeTracker timer = new(0);

    private void OnEnable()
    {
        timer.SetTimer(lifeTime);
    }

    void Update()
    {
        timer.Update(Time.deltaTime);
        gameObject.SetActive(timer.tick > 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Decrease health through VariableManager.
            timer.Finish();
        }
    }
}
