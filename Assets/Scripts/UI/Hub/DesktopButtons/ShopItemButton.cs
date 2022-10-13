using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SaveManager.VariableToSave;

[RequireComponent(typeof(Image))]
public abstract class ShopItemButton : BaseButton
{
    protected ShopButtonContainer container;
    protected TextMeshProUGUI label;
    private Image image;
    protected BaseShopItem item;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        container = GetComponentInParent<ShopButtonContainer>();
        label = GetComponentInChildren<TextMeshProUGUI>();
        ButtonDeselected();
    }

    public virtual void StartUp(BaseShopItem item)
    {
        this.item = item;
        transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;
        UpdateValues();
    }

    #region mouse hover and etc.
    [Header("Mouse events")]
    [SerializeField] private Color normal = new(0.1254902f, 0.1333333f, 0.145098f);
    [SerializeField] private Color onHover = new(0.254902f, 0.254902f, 0.254902f);
    [SerializeField] private Color selected = new(0.345098f, 0.3960784f, 0.9490196f);

    public override void OnPointerEnter(PointerEventData eventData)
    {
        UpdateColour(onHover);
    }

    protected override void OnClick()
    {
        if (container.selectedItem != this)
        {
            image.color = selected;
            container.UpdateSelectedButton(this);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        UpdateColour(normal);
    }

    private void UpdateColour(Color colourState)
    {
        if (container.selectedItem != this)
        {
            image.color = colourState;
        }
    }

    public void ButtonDeselected()
    {
        image.color = normal;
    }
    #endregion

    public string RichDescription => item.RichDescription;
    public abstract bool CanPurchase { get; }
    protected bool HasSufficientMoney()
    {
        VariableManager var = GameManager.VariableManager;
        bool sufficientMoney = var.GetVariable<int>(bytecoins) >= item.GetBytecoinPrice;
        sufficientMoney = sufficientMoney && var.GetVariable<int>(intelligenceData) >= item.GetIntelligenceDataPrice;
        sufficientMoney = sufficientMoney && var.GetVariable<int>(processingPower) >= item.GetProcessingPowerPrice;
        return sufficientMoney;
    }

    public void UpdateValues()
    {
        label.text = item.Label;
    }

    public void Purchase()
    {
        GameManager.VariableManager.Purchase(item.GetBytecoinPrice, item.GetIntelligenceDataPrice, item.GetProcessingPowerPrice);
        item.UpdatePurchase();
        GameManager.VariableManager.InvokePurchase();
        GameManager.SaveManager.SaveToFile();
        UpdateValues();
    }
}
