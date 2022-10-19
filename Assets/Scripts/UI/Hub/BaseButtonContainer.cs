using UnityEngine;

public abstract class BaseButtonContainer : MonoBehaviour
{
    public BaseItemButton SelectedItem { get; private set; }
    [field: SerializeField] protected GameObject BaseItem { get; private set; }

    //Used in toggle application window button on click.
    public void ResetButton()
    {
        UpdateSelectedButton();
    }

    public virtual void UpdateSelectedButton(BaseItemButton storeButton = null)
    {
        if (SelectedItem != null)
        {
            SelectedItem.ButtonDeselected();
        }
        SelectedItem = storeButton;
    }
}
