using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static VariableManager.AllUnlockables;

public class PhishingGameplayScreen : MonoBehaviour
{
    private readonly TimeTracker computerSpawnTimer = new(2);
    private readonly TimeTracker antivirusSpawnTimer = new(1);
    public int Score { get; private set; } = 0;
    [SerializeField] private PhishingStartScreen titleScreen;
    [SerializeField] private Computer computer;
    [SerializeField] private AntiVirus antiVirus;
    [SerializeField] private TextMeshProUGUI scoreboard;
    [SerializeField] private List<GameObject> removables = new();

    private void OnEnable()
    {
        foreach(GameObject go in removables)
        {
            Destroy(go);
        }

        Score = 0;
        UpdateScore();
    }

    private void Start()
    {
        computerSpawnTimer.onFinish += () =>
        {
            computerSpawnTimer.SetTimer(Random.Range(1.5f, 2.5f));
            Computer go = Instantiate(computer, transform);

            Vector3 pos = new()
            {
                x = 15,
                y = Random.Range(-3.5f, 3.5f),
            };
            go.transform.localPosition = pos;
            removables.Add(go.gameObject);
            computerSpawnTimer.Reset();
        };

        antivirusSpawnTimer.onFinish += () =>
        {
            antivirusSpawnTimer.SetTimer(Random.Range(0.75f, 1.25f));
            Transform virus = Instantiate(antiVirus, new(15, Random.Range(3.5f, -3.5f), 0), transform.rotation).transform;
            virus.parent = transform;
            removables.Add(virus.gameObject);
            antivirusSpawnTimer.Reset();
        };
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        computerSpawnTimer.Update(delta);
        antivirusSpawnTimer.Update(delta);
    }

    private void UpdateScore()
    {
        scoreboard.text = $"Computers Phished: {Score}\nHigh Score: {GameManager.VariableManager.GetMinigameScore(PhishingMinigame)}";
    }

    public void IncreaseScore()
    {
        Score++;
        UpdateScore();
    }

    public void GameOver()
    {
        titleScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}