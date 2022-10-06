using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SaveManager.VariableToSave;

[RequireComponent(typeof(Image))]
public class StoreItemButton : BaseButton
{
    private StoreButtonContainer container;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private ShopItem item;

    public bool CanPurchase => !item.Item.IsUnlocked && HasSufficientMoney();
    public string ItemRichDescription
    {
        get
        {
            string itemRichDescription = $"<align=center><size=16><b>{item.ItemName}</b></size></align>\n\n{item.Description}";
            if (!item.Item.IsUnlocked)
            {
                VariableManager var = GameManager.VariableManager;
                AppendDescription(ref itemRichDescription, var.GetVariable<int>(bytecoins), item.BytecoinPrice, "\nByteCoins:\t");
                AppendDescription(ref itemRichDescription, var.GetVariable<int>(intelligenceData), item.IntelligenceDataPrice, "Intelligence Data:");
                AppendDescription(ref itemRichDescription, var.GetVariable<int>(processingPower), item.ProcessingPowerPrice, "Processing Power:");
            }
            return itemRichDescription;
        }
    }

    [Header("Mouse events")]
    [SerializeField] private Color normal = Color.black;
    [SerializeField] private Color onHover = Color.gray;
    [SerializeField] private Color selected = Color.white;
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        container = GetComponentInParent<StoreButtonContainer>();
        ButtonDeselected();
        UpdateValues();
    }

    #region mouse hover and etc.
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

    public void UpdateValues()
    {
        label.text = item.Label;
    }

    private void AppendDescription(ref string rich, int currentAmount, int cost, string variableName)
    {
        string prefix = "";
        string suffix = $"{cost}";
        if (currentAmount < cost)
        {
            prefix = $"<color=\"red\">";
            suffix += $"</color>";
        }

        rich += $"\n{prefix}{variableName}\t\t{suffix}";
    }

    private bool HasSufficientMoney()
    {
        VariableManager var = GameManager.VariableManager;
        bool sufficientMoney = var.GetVariable<int>(bytecoins) >= item.BytecoinPrice;
        sufficientMoney = sufficientMoney && var.GetVariable<int>(intelligenceData) >= item.IntelligenceDataPrice;
        sufficientMoney = sufficientMoney && var.GetVariable<int>(processingPower) >= item.ProcessingPowerPrice;
        return sufficientMoney;
    }

    public void Purchase()
    {
        item.Item.Unlock();
        GameManager.VariableManager.Purchase(item.BytecoinPrice, item.IntelligenceDataPrice, item.ProcessingPowerPrice);
        UpdateValues();
    }
}
