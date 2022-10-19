using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ItemDescription : MonoBehaviour
{
    private TextMeshProUGUI info;
    private string startingInfo;

    void Start()
    {
        info = GetComponent<TextMeshProUGUI>();
        startingInfo = info.text;
    }

    public void UpdateInformation(string description)
    {
        info.text = description ?? startingInfo;
        ((RectTransform)transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, info.preferredHeight);
    }
}
