using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractObjective : BaseObjective
{
    protected override void Start()
    {
        base.Start();
        GetComponent<Interactable>().AddInteraction(ObjectiveFinish);
    }
}
