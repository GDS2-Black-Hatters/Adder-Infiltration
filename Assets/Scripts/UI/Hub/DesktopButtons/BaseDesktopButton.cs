using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public abstract class BaseDesktopButton : BaseButton
{
    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("IsHovering", true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("IsHovering", false);
    }
}
