using System;
using UnityEngine;

public class SliderDefaultButton : BaseButton
{
    [SerializeField] private BaseSlider[] sliders;
    private Action onClickAction;

    protected override void Awake()
    {
        base.Awake();
        foreach (BaseSlider slider in sliders)
        {
            onClickAction += slider.ResetToDefualt;
        }
    }

    protected override void OnClick()
    {
        onClickAction?.Invoke();
    }
}
