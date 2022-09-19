using UnityEngine;

public class QuitButton : BaseButton
{
    protected override void OnClick()
    {
        Application.Quit();
    }
}
