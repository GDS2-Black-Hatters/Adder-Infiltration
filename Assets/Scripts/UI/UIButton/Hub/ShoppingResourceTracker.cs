using TMPro;
using UnityEngine;

public class ShoppingResourceTracker : MonoBehaviour
{
    private TextMeshProUGUI resource;

    // Start is called before the first frame update
    private void Start()
    {
        resource = GetComponent<TextMeshProUGUI>();
        UpdateValues();
    }

    private void UpdateValues()
    {
        VariableManager var = GameManager.VariableManager;
        resource.text = $"Bytecoins: {var.byteCoins:000}\tIntelligence Data: {var.intelligenceData:000}\tProcessing Power: {var.processingPower:000}";
    }
}
