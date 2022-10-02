using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public abstract class BaseDesktopButton : BaseButton
{
    private Animator anim;
    [SerializeField] private TooltipBehaviour tooltip;
    [SerializeField] protected string tooltipTitle;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("IsHovering", true);
        tooltip.ShowText(tooltipTitle, transform);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("IsHovering", false);
        tooltip.ShowText();
    }
}
