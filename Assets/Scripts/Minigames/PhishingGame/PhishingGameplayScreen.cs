using UnityEngine;
using static VariableManager.AllUnlockables;

public class PhishingGameplayScreen : MinigameGameplayScreen
{
    private readonly TimeTracker computerSpawnTimer = new(2);
    private readonly TimeTracker antivirusSpawnTimer = new(1);
    public int Score { get; private set; } = 0;
    [SerializeField] private Computer computer;
    [SerializeField] private AntiVirus antiVirus;

    protected override void OnEnable()
    {
        Score = 0;
        base.OnEnable();
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

    protected override void UpdateScore()
    {
        scoreboard.text = $"Computers Phished: {Score}\nHigh Score: {GameManager.VariableManager.GetMinigameScore(PhishingMinigame)}";
    }

    public void IncreaseScore()
    {
        Score++;
        UpdateScore();
    }
}