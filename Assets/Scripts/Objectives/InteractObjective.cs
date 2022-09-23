using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractObjective : BaseObjective
{
    [SerializeField] private string _objectiveText = "Hack into device";
    public override string objectiveTitle { get { return _objectiveText; }}

    private Interactable interactable;

    protected override void Start()
    {
        base.Start();
        interactable = GetComponent<Interactable>();
        interactable.AddInteraction(Interact);
    }

    private void Interact()
    {
        ObjectiveCompleteSound.Post(gameObject);
        Destroy(this);
        Destroy(interactable);
    }
}
