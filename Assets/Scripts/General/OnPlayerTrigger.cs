using UnityEngine;

public class OnPlayerTrigger : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter.Invoke();
        }
    }
}
