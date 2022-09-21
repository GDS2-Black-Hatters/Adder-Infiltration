using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ItemDescription : MonoBehaviour
{
    private TextMeshProUGUI info;

    void Start()
    {
        info = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateInformation(string description)
    {
        info.text = description;
        ((RectTransform)transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, info.preferredHeight);
    }
}
