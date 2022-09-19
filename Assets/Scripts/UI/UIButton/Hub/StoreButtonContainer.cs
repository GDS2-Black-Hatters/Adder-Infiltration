using UnityEngine;

public class StoreButtonContainer : MonoBehaviour
{
    public BaseStoreButton selectedButton { get; private set; }

    public void UpdateSelectedButton(BaseStoreButton storeButton)
    {
        if (selectedButton != null)
        {
            selectedButton.ButtonDeselected();
        }
        selectedButton = storeButton;
    }
}
