public class SaveButton : BaseButton
{
    protected override void OnClick()
    {
        GameManager.SaveManager.SaveToFile(false);
    }
}
