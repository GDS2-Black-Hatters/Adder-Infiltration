using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class BaseSlider : MonoBehaviour
{
    protected Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }
    protected abstract void OnValueChanged(float value);
}
