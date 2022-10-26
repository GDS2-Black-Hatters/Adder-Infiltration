using static SaveManager.VariableToSave;

public class BackgroundChangeButton : BaseItemButton
{
    private Background background;
    private BackgroundSwapper swapper;

    public void StartUp(BaseButtonContainer container, BaseItem item, BackgroundSwapper swapper)
    {
        base.StartUp(container, item);
        background = (Background)item;
        this.swapper = swapper;
        if (IsBackground())
        {
            base.OnClick();
        }
    }

    protected override void OnClick()
    {
        if (!IsBackground())
        {
            base.OnClick();
            swapper.UpdateBackground(background.Unlockable);
            GameManager.VariableManager.SetVariable(currentDesktopBackground, background.Unlockable);
            GameManager.SaveManager.SaveToFile(false);
        }
    }

    private bool IsBackground()
    {
        return GameManager.VariableManager.GetVariable<VariableManager.AllUnlockables>(currentDesktopBackground) == background.Unlockable;
    }
}
