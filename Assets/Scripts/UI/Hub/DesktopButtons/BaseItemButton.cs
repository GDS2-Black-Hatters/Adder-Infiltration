using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class BaseItemButton : BaseButton
{
    private BaseButtonContainer container;
    private Image image;
    protected BaseItem item;
    private TextMeshProUGUI label;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        label = GetComponentInChildren<TextMeshProUGUI>();
        ButtonDeselected();
    }

    public virtual void StartUp(BaseButtonContainer container, BaseItem item)
    {
        this.container = container;
        this.item = item;
        transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;
        UpdateValues();
    }

    protected override void OnClick()
    {
        if (container.SelectedItem != this)
        {
            image.color = selected;
            container.UpdateSelectedButton(this);
        }
    }

    public void UpdateValues()
    {
        label.text = item.Label;
    }

    #region mouse hover and etc.
    [Header("Mouse events")]
    [SerializeField] protected Color normal = new(0.1254902f, 0.1333333f, 0.145098f);
    [SerializeField] protected Color onHover = new(0.254902f, 0.254902f, 0.254902f);
    [SerializeField] protected Color selected = new(0.345098f, 0.3960784f, 0.9490196f);

    public override void OnPointerEnter(PointerEventData eventData)
    {
        UpdateColour(onHover);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        UpdateColour(normal);
    }

    protected void UpdateColour(Color colourState)
    {
        if (container.SelectedItem != this)
        {
            image.color = colourState;
        }
    }

    public void ButtonDeselected()
    {
        image.color = normal;
    }
    #endregion
}
