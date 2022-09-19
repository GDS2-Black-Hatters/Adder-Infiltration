using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public abstract class BaseDesktopButton : BaseButton, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("IsHovering", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("IsHovering", false);
    }
}
