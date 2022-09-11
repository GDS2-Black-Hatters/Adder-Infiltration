using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class DesktopIconBehaviour : UIButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;

    protected override void Start()
    {
        base.Start();
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
