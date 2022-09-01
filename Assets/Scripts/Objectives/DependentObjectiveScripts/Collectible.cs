using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    protected Action onCollect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onCollect?.Invoke();
            Destroy(gameObject);
        }
    }
}
