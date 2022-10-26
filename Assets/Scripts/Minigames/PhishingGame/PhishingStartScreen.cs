public class PhishingStartScreen : MinigameStartScreen
{
    private PhishingGameplayScreen phishingGameplayScreen;

    protected override void Start()
    {
        base.Start();
        phishingGameplayScreen = (PhishingGameplayScreen)gameplayScreen;
    }

    protected override int GetScore()
    {
        return phishingGameplayScreen.Score;
    }
}
