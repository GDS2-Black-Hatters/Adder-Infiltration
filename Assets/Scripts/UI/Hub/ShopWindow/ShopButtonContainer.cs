using UnityEngine;
using UnityEngine.UI;

public class ShopButtonContainer : BaseButtonContainer
{
    private ShopItemButton ShopItem => (ShopItemButton)SelectedItem;
    [SerializeField] private ItemDescription itemInfo;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private ShopItemList itemList;
    [SerializeField] private Scrollbar rightSideScrollBar;

    private void Awake()
    {
        purchaseButton.interactable = false;
        purchaseButton.onClick.AddListener(() =>
        {
            ShopItem.Purchase();
            UpdateValues();
        });

        foreach (BaseShopItem item in itemList.GetItems())
        {
            GameObject go = Instantiate(BaseItem, transform);
            switch (item)
            {
                case ShopItem shopItem:
                    go.AddComponent<PurchaseItemButton>().StartUp(this, item);
                    break;

                case AbilityItem abilityItem:
                    go.AddComponent<PurchaseAbilityButton>().StartUp(this, item);
                    break;
            }
        }
    }

    public override void UpdateSelectedButton(BaseItemButton storeButton = null)
    {
        rightSideScrollBar.value = 1;
        base.UpdateSelectedButton(storeButton);
        UpdateValues();
    }

    private void UpdateValues()
    {
        if (SelectedItem)
        {
            itemInfo.UpdateInformation(ShopItem.RichDescription);
            purchaseButton.interactable = ShopItem.CanPurchase;
        }
        else
        {
            itemInfo.UpdateInformation(null);
            purchaseButton.interactable = false;
        }
    }
}