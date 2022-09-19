using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StoreItemButton : BaseButton, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private StoreButtonContainer container;
    [SerializeField] private Color normal = Color.black;
    [SerializeField] private Color onHover = Color.gray;
    [SerializeField] private Color selected = Color.white;
    private Image image;

    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string itemName;
    [TextArea(5, 5), SerializeField] private string itemDescription;
    public string itemRichDescription { get; private set; }

    [Header("Base Cost")]
    [SerializeField] private int baseBytecoinCost = 1;
    [SerializeField] private int baseIntelligenceDataCost = 1;
    [SerializeField] private int baseProcessingPowerCost = 1;
    private int bytecoinCost;
    private int intelligenceDataCost;
    private int processingPowerCost;

    private int abilityLevel = 0;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        ButtonDeselected();
        UpdateValues();
    }

    #region mouse hover and etc.
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateColour(onHover);
    }

    protected override void OnClick()
    {
        if (container.selectedButton != this)
        {
            image.color = selected;
            container.UpdateSelectedButton(this);
        }
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
    #endregion

    public void UpdateValues()
    {
        VariableManager var = GameManager.VariableManager;

        //////////////////////////////////////////////////////TODO: Set ability level to loaded variable.
        int costMultiplier = abilityLevel + 1;
        bytecoinCost = costMultiplier * baseBytecoinCost;
        intelligenceDataCost = costMultiplier * baseIntelligenceDataCost;
        processingPowerCost = costMultiplier * baseProcessingPowerCost;

        //Update label text.
        label.text = $"{itemName}\nLevel {abilityLevel}";

        //Modify item description into rich text.
        itemRichDescription = $"<align=center><size=16><b>{itemName}</b></size></align>\nLevel {abilityLevel}\n{itemDescription}\nCosts:";
        AppendDescription(var.byteCoins, bytecoinCost, "ByteCoins:\t");
        AppendDescription(var.intelligenceData, intelligenceDataCost, "Intelligence Data:");
        AppendDescription(var.processingPower, processingPowerCost, "Processing Power:");
    }

    private void AppendDescription(int currentAmount, int cost, string variableName)
    {
        if (cost == 0)
        {
            return;
        }

        string prefix = "";
        string suffix = "";
        if (currentAmount < cost)
        {
            prefix = $"<color=\"red\">";
            suffix = $"{cost}</color>";
        }

        itemRichDescription += $"\n\t{prefix}{variableName}\t\t{suffix}";
    }
}
