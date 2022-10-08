using TMPro;
using UnityEngine;

public class TooltipBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;

    public void ShowText(string text = "No name", Transform newParent = null)
    {
        gameObject.SetActive(newParent);
        if (!newParent)
        {
            return;
        }
        textComponent.text = text;
        transform.SetParent(newParent);
        ((RectTransform)transform).anchoredPosition = Vector2.zero;
        transform.SetParent(newParent.parent); //Detach it because it becomes part of the button hence recast block.
    }
}
