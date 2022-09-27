using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TintableButton : BaseButton
{
    private Image image;
    [SerializeField] private Color normalColour = new(0, 0, 0, 0);
    [SerializeField] private Color hoverColour = new(1, 0, 0);

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        image.color = normalColour;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColour;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColour;
    }

    protected override void OnClick() {} //Not meant to have any.
}
