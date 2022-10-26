public class RunStartScreen : MinigameStartScreen
{
    private RunGameplayScreen runGameplayScreen;

    protected override void Start()
    {
        base.Start();
        runGameplayScreen = (RunGameplayScreen)gameplayScreen;
    }

    protected override int GetScore()
    {
        return (int)runGameplayScreen.distanceRan;
    }
}
