using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonContainer : MonoBehaviour
{
    public ShopItemButton selectedItem { get; private set; }
    [SerializeField] private ItemDescription itemInfo;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private GameObject BaseItem;
    [SerializeField] private List<BaseShopItem> items;

    private void Awake()
    {
        purchaseButton.interactable = false;
        purchaseButton.onClick.AddListener(() =>
        {
            selectedItem.Purchase();
            UpdateValues();
        });

        foreach (BaseShopItem item in items)
        {
            GameObject go = Instantiate(BaseItem, transform);
            switch (item)
            {
                case ShopItem shopItem:
                    go.AddComponent<PurchaseItemButton>().StartUp(item);
                    break;

                case AbilityItem abilityItem:
                    go.AddComponent<PurchaseAbilityButton>().StartUp(item);
                    break;
            }
        }
    }

    public void UpdateSelectedButton(ShopItemButton storeButton)
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