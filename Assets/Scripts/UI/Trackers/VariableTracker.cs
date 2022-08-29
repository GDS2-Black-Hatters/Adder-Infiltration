using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VariableTracker : MonoBehaviour
{
    protected Image ui;

    protected virtual void Start()
    {
        ui = GetComponent<Image>();
    }

    protected void Update()
    {
        UpdateUI();
    }

    protected virtual void UpdateUI() {}
}
