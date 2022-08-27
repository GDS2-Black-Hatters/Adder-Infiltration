using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }
}
