using UnityEngine;

public class PanelChangeButton : BaseButton
{
    [SerializeField] private GameObject toEnable;
    [SerializeField] private GameObject toDisable;

    protected override void OnClick()
    {
        toEnable.SetActive(true);
        toDisable.SetActive(false);
    }
}
