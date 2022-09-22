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
        resource.text = $"Bytecoins: {var.Bytecoins:000}\tIntelligence Data: {var.IntelligenceData:000}\tProcessing Power: {var.ProcessingPower:000}";
    }
}
