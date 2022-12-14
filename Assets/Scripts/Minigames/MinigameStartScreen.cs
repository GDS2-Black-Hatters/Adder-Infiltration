using TMPro;
using UnityEngine;
using static VariableManager;
using static SaveManager.VariableToSave;

public abstract class MinigameStartScreen : MonoBehaviour
{
    [SerializeField] protected AllUnlockables minigame;
    [SerializeField] protected MinigameGameplayScreen gameplayScreen;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private TextMeshProUGUI textToBlink;
    private readonly TimeTracker blinkText = new(1);
    private bool isTitleScreen = true;

    private void OnEnable()
    {
        if (isTitleScreen)
        {
            isTitleScreen = false;
            return;
        }
        textToBlink.text = "Press space again to retry!";
        CalcuateFinalScore();
    }

    protected virtual void Start()
    {
        blinkText.onFinish += () =>
        {
            textToBlink.gameObject.SetActive(!textToBlink.gameObject.activeInHierarchy);
            blinkText.SetTimer(textToBlink.gameObject.activeInHierarchy ? 1 : 0.5f);
        };
    }

    private void Update()
    {
        blinkText.Update(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameplayScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    protected abstract int GetScore();

    private void CalcuateFinalScore()
    {
        int coin = 0;
        int data = 0;
        int power = 0;
        for (int i = 0; i < GetScore(); i++)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    coin++;
                    break;

                case 1:
                    data++;
                    break;

                case 2:
                    power++;
                    break;
            }
        }

        VariableManager var = GameManager.VariableManager;
        int curr1 = var.GetVariable<int>(bytecoins);
        int curr2 = var.GetVariable<int>(intelligenceData);
        int curr3 = var.GetVariable<int>(processingPower);
        var.SetVariable(bytecoins, curr1 + coin);
        var.SetVariable(intelligenceData, curr2 + data);
        var.SetVariable(processingPower, curr3 + power);
        string highScoreText = var.UpdateMinigameHighScore(minigame, GetScore()) ?
            " and that is a new high score!" : "";

        bodyText.text = $"Your score was {GetScore()}{highScoreText}\n" +
            $"Bytecoins:\t\t{curr1} -> {curr1 + coin}\n" +
            $"Intelligence Data:\t{curr2} -> {curr2 + data}\n" +
            $"Processing Power:\t{curr3} -> {curr3 + power}";
    }
}
