using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BaseStoreButton : BaseButton, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected StoreButtonContainer container;
    [SerializeField] protected Color normal = Color.black;
    [SerializeField] protected Color onHover = Color.gray;
    [SerializeField] protected Color selected = Color.white;
    protected Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        ButtonDeselected();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateColour(onHover);
    }

    protected override void OnClick()
    {
        image.color = selected;
        container.UpdateSelectedButton(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpdateColour(normal);
    }

    private void UpdateColour(Color colourState)
    {
        if (container.selectedButton != this)
        {
            image.color = colourState;
        }
    }

    public void ButtonDeselected()
    {
        image.color = normal;
    }
}
