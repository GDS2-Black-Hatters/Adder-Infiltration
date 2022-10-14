using UnityEngine;

public class MouseChangeButton : BaseItemButton
{
    private Mouse mouse;

    public override void StartUp(BaseButtonContainer container, BaseItem item)
    {
        base.StartUp(container, item);
        mouse = (Mouse)item;
    }

    protected override void OnClick()
    {
        base.OnClick();
        Cursor.SetCursor(mouse.Frames[0], Vector2.zero, CursorMode.Auto);
    }
}
