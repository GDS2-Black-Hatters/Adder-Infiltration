using TMPro;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    private TextMeshProUGUI resource;

    private void Start()
    {
        resource = GetComponent<TextMeshProUGUI>();
        UpdateValues();
    }

    public void UpdateValues()
    {
        VariableManager var = GameManager.VariableManager;
        resource.text = $"Bytecoins: {var.bytecoins:000}\tIntelligence Data: {var.intelligenceData:000}\tProcessing Power: {var.processingPower:000}";
    }
}
