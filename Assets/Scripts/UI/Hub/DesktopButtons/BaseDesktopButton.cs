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
        UpdateButton(true, tooltipTitle, transform);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        UpdateButton(false);
    }

    private void UpdateButton(bool isHovering, string title = null, Transform newParent = null)
    {
        anim.SetBool("IsHovering", isHovering);
        if (tooltip)
        {
            tooltip.ShowText(title, newParent);
        }
    }
}
