using UnityEngine;
using UnityEngine.UI;

public class StoreButtonContainer : MonoBehaviour
{
    public StoreItemButton selectedItem { get; private set; }
    [SerializeField] private ItemDescription itemInfo;
    [SerializeField] private Button purchaseButton;

    private void Awake()
    {
        purchaseButton.interactable = false;
        purchaseButton.onClick.AddListener(() =>
        {
            selectedItem.Purchase();
            UpdateValues();
        });
    }

    public void UpdateSelectedButton(StoreItemButton storeButton)
    {
        if (selectedItem != null)
        {
            selectedItem.ButtonDeselected();
        }
        selectedItem = storeButton;
        UpdateValues();
    }

    private void UpdateValues()
    {
        itemInfo.UpdateInformation(selectedItem.RichDescription);
        purchaseButton.interactable = selectedItem.CanPurchase;
    }
}
