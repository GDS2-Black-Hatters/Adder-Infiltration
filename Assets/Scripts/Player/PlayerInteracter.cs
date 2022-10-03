using System.Collections.Generic;
using UnityEngine;
using static InputManager;
using static ActionInputSubscriber.CallbackContext;

public class PlayerInteracter : MonoBehaviour
{
    [Header("Interaction Area")]
    [SerializeField] private Transform castSourceTransform;
    [SerializeField] private float interactSphereCastRadius = 1.5f;
    [SerializeField] private float interactMaxDistance = 10f;
    [SerializeField] private LayerMask interactionMask;

    private Interactable focusInteractable;

    [Header("Interaction Line")]
    [SerializeField] private LineRenderer interactionLinePrefab;
    [SerializeField] private float lineWidthCap = 0.1f;
    [SerializeField] private float lineShrinkSpeed = 0.05f;
    private Dictionary<Interactable, LineRenderer> interactLines = new();
    
    private void Start()
    {
        GameManager.LevelManager.player.virusController.onInteractStart += InteractStart;
        GameManager.LevelManager.player.virusController.onInteractEnd += InteractHalt;
    }

    private void Update()
    {
        UpdateInteractableFocus();
        UpdateLines();
    }

    /// <summary>
    /// updates the focused interactable variable, calls focus and unfocus on the interactables if focus has changed.
    /// </summary>
    private void UpdateInteractableFocus()
    {
        Interactable newFocusInteractable = GetMostFocusedInteractable();
        if (focusInteractable != newFocusInteractable)
        {
            if(focusInteractable != null) focusInteractable.OnUnfocus();
            if(newFocusInteractable != null) newFocusInteractable.OnFocus();
            focusInteractable = newFocusInteractable;

            if (focusInteractable == null) return;

            if (!interactLines.ContainsKey(focusInteractable))
            {
                interactLines[focusInteractable] = Instantiate(interactionLinePrefab, transform);
                interactLines[focusInteractable].startWidth = lineWidthCap;
                interactLines[focusInteractable].endWidth = lineWidthCap;
            }
        }
    }

    private void UpdateLines()
    {
        List<Interactable> linesToRemove = new();

        foreach (KeyValuePair<Interactable, LineRenderer> line in interactLines)
        {
            if (line.Key == null)
            {
                linesToRemove.Add(line.Key);
                Destroy(line.Value.gameObject);
                continue;
            }

            line.Value.SetPositions(new Vector3[] { line.Key.transform.position, transform.position });

            //Widen line width to cap if focus, else shrink it and mark for destroy if small enough
            float curLineWidth = line.Value.startWidth;
            if (line.Key == focusInteractable)
                curLineWidth = lineWidthCap;
            else
            {
                curLineWidth -= Time.deltaTime * lineShrinkSpeed;
                if (curLineWidth <= 0)
                {
                    linesToRemove.Add(line.Key);
                    Destroy(line.Value.gameObject);
                    continue;
                }
            }
            line.Value.startWidth = curLineWidth;
            line.Value.endWidth = curLineWidth;
        }

        foreach (Interactable inter in linesToRemove)
        {
            interactLines.Remove(inter);
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
            if (rch.transform.TryGetComponent(out Interactable thisInter))
            {
                float thisDotProduct = Vector3.Dot(castSourceTransform.forward, (rch.transform.position - castSourceTransform.position).normalized);
                if (thisDotProduct > bestDotProduct)
                {
                    bfInteractable = thisInter;
                    bestDotProduct = thisDotProduct;
                }
            }
        }

        return bfInteractable;
    }

    public void InteractStart()
    {
        if (focusInteractable == null) return;

        focusInteractable.InteractStart();
    }

    public void InteractHalt()
    {
        if (focusInteractable == null) return;

        focusInteractable.InteractHalt();
    }
}
