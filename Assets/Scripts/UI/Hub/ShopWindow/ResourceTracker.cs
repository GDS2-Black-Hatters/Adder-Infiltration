using TMPro;
using UnityEngine;
using static SaveManager.VariableToSave;

public class ResourceTracker : MonoBehaviour
{
    private TextMeshProUGUI resource;

    private void Start()
    {
        resource = GetComponent<TextMeshProUGUI>();
        UpdateValues();
    }

    private void OnEnable()
    {
        GameManager.VariableManager.purchaseCallback += UpdateValues;
    }

    private void OnDisable()
    {
        GameManager.VariableManager.purchaseCallback -= UpdateValues;
    }

    public void UpdateValues()
    {
        VariableManager var = GameManager.VariableManager;
        resource.text = $"" +
            $"Bytecoins: {var.GetVariable<int>(bytecoins):000}\t" +
            $"Intelligence Data: {var.GetVariable<int>(intelligenceData):000}\t" +
            $"Processing Power: {var.GetVariable<int>(processingPower):000}";
    }
}
