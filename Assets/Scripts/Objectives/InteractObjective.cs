using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractObjective : BaseObjective
{
    private Interactable interactable;

    protected override void Start()
    {
        base.Start();
        objectiveTitle = "Hack into " + transform.name;
        interactable = GetComponent<Interactable>();
        interactable.AddInteraction(Interact);
    }

    private void Interact()
    {
        Destroy(this);
        Destroy(interactable);
    }
}
