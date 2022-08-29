using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent onInteract;

    public void OnFocus()
    {
        //Debug.Log("Focus: " + transform.parent.name);
    }

    public void OnUnfocus()
    {
        //Debug.Log("Unfocus: " + transform.parent.name);
    }

    public void Interact()
    {
        onInteract.Invoke();
    }
}
