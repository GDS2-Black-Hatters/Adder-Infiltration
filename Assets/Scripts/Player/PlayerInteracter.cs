using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracter : MonoBehaviour
{
    [SerializeField] private Transform castSourceTransform;
    [SerializeField] private float interactSphereCastRadius = 1.5f;
    [SerializeField] private float interactMaxDistance = 10f;
    [SerializeField] private LayerMask interactionMask;

    public void Interact(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        //Debug.Log("Interact Initiated");
        RaycastHit[] interactables = Physics.SphereCastAll(castSourceTransform.position, interactSphereCastRadius, castSourceTransform.forward, interactMaxDistance, interactionMask.value);
        
        //Debug.Log("Hit result count: " + interactables.Length);
        //Loop through all spherecasthits to find the Interactable monobehavior closest to raycast direction
        Interactable bfInteractable = null;
        float bestDotProduct = float.MinValue;
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

        //Debug.Log("Interact Search loop complete");
        //return if no Interactable found
        if(bfInteractable == null) return;

        //Debug.Log("Calling Target Interact");
        bfInteractable.Interact();
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
