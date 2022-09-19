using TMPro;
using UnityEngine;

public class ShoppingBody : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemInfo;

    public void UpdateInfo(string info)
    {
        itemInfo.text = info;
    }
}
