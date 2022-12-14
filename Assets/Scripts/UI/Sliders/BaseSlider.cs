using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class BaseSlider : MonoBehaviour
{
    protected Slider slider;
    protected float defaultValue;
    [SerializeField] protected SliderDefaultButton dedicatedDefaultButton;
    [SerializeField] private SaveManager.VariableToSave settingVariable;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
        defaultValue = slider.value;
        slider.onValueChanged.AddListener(OnValueChanged);
        slider.value = GameManager.VariableManager.GetVariable<float>(settingVariable);
    }

    public void ResetToDefualt()
    {
        slider.value = defaultValue;
    }

    protected virtual void OnValueChanged(float value)
    {
        dedicatedDefaultButton.gameObject.SetActive(value != defaultValue);
        GameManager.VariableManager.SetVariable(settingVariable, value);
    }
}
