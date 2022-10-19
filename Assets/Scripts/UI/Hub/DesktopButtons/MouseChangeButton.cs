using static SaveManager.VariableToSave;

public class MouseChangeButton : BaseItemButton
{
    private Mouse mouse;

    public override void StartUp(BaseButtonContainer container, BaseItem item)
    {
        base.StartUp(container, item);
        mouse = (Mouse)item;
        if (IsMouse())
        {
            base.OnClick();
        }
    }

    protected override void OnClick()
    {
        if (!IsMouse())
        {
            base.OnClick();
            GameManager.InputManager.UpdateMouse(mouse);
            GameManager.VariableManager.SetVariable(mouseSprite, mouse.Unlockable);
            GameManager.SaveManager.SaveToFile(false);
        }
    }

    private bool IsMouse()
    {
        return GameManager.VariableManager.GetVariable<VariableManager.AllUnlockables>(mouseSprite) == mouse.Unlockable;
    }
}
