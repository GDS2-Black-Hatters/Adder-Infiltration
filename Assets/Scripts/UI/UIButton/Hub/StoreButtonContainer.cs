using UnityEngine;

public class StoreButtonContainer : MonoBehaviour
{
    public StoreItemButton selectedButton { get; private set; }
    [SerializeField] private ShoppingBody shoppingBody;

    public void UpdateSelectedButton(StoreItemButton storeButton)
    {
        if (selectedButton != null)
        {
            selectedButton.ButtonDeselected();
        }
        selectedButton = storeButton;
        shoppingBody.UpdateInfo(selectedButton.itemRichDescription);
    }
}
