using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    protected Action onCollect;
    [SerializeField] protected AK.Wwise.Event collectSFXEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectSFXEvent.Post(gameObject);
            onCollect?.Invoke();
            Destroy(gameObject);
        }
    }
}
