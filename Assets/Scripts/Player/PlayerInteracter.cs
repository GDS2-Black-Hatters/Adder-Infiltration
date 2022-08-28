using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracter : MonoBehaviour
{
    [SerializeField] private Transform castSourceTransform;
    [SerializeField] private float interactSphereCastRadius = 1.5f;
    [SerializeField] private float interactMaxDistance = 10f;
    [SerializeField] private LayerMask interactionMask;

    private Interactable focusInteractable;

    private void Update()
    {
        UpdateInteractableFocus();
    }

    /// <summary>
    /// updates the focused interactable variable, calls focus and unfocus on the interactables if focus has changed.
    /// </summary>
    private void UpdateInteractableFocus()
    {
        Interactable newFocusInteractable = GetMostFocusedInteractable();
        if(focusInteractable != newFocusInteractable)
        {
            focusInteractable?.OnUnfocus();
            newFocusInteractable?.OnFocus();
            focusInteractable = newFocusInteractable;
        }
    }

    /// <summary>
    /// tries to get the Interactable monobehavior closest to the center of the screen, will return null if no Interactable is within bounds.
    /// </summary>
    private Interactable GetMostFocusedInteractable()
    {
        RaycastHit[] interactables = Physics.SphereCastAll(castSourceTransform.position, interactSphereCastRadius, castSourceTransform.forward, interactMaxDistance, interactionMask.value);
        
        //Loop through all spherecasthits to find the Interactable monobehavior closest to raycast direction
        Interactable bfInteractable = null;
        float bestDotProduct = 0;
        foreach (RaycastHit rch in interactables)
        {
            Interactable thisInter;
            if(rch.transform.TryGetComponent<Interactable>(out thisInter))
            {
                float thisDotProduct = Vector3.Dot(castSourceTransform.forward, (rch.transform.position - castSourceTransform.position).normalized);
                if(thisDotProduct > bestDotProduct)
                {
                    bfInteractable = thisInter;
                    bestDotProduct = thisDotProduct;
                }
            }
        }

        return bfInteractable;
    }

    public void Interact(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if(focusInteractable == null) return;

        focusInteractable.Interact();
    }

    private void OnEnable()
    {
        GameManager.InputManager.GetAction(InputManager.Controls.Interact).performed += Interact;
    }

    private void OnDisable()
    {
        GameManager.InputManager.GetAction(InputManager.Controls.Interact).performed -= Interact;
    }
}
