using UnityEngine;
using UnityEngine.UI;

public class StoreButtonContainer : MonoBehaviour
{
    public StoreItemButton selectedItem { get; private set; }
    [SerializeField] private ItemDescription itemInfo;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private ResourceTracker shoppingResourceTracker;
    [SerializeField] private ResourceTracker shoppingResourceTracker2; //Todo: FIX ME!

    private void Awake()
    {
        purchaseButton.interactable = false;
        purchaseButton.onClick.AddListener(() =>
        {
            selectedItem.Purchase();
            UpdateValues();
            shoppingResourceTracker.UpdateValues();
            shoppingResourceTracker2.UpdateValues();
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
        itemInfo.UpdateInformation(selectedItem.ItemRichDescription);
        purchaseButton.interactable = selectedItem.CanPurchase;
    }
}
