using TMPro;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    private TimeTracker computerSpawnTimer = new(2, -1, true);
    private TimeTracker antivirusSpawnTimer = new(1, -1, true);

    private int score = 0;
    [SerializeField] private Computer computer;
    [SerializeField] private AntiVirus antiVirus;
    [SerializeField] private TextMeshProUGUI scoreboard;

    private void Start()
    {
        computerSpawnTimer.onFinish += () =>
        {
            computerSpawnTimer.SetTimer(Random.Range(1.5f, 2.5f));
            Computer go = Instantiate(computer, transform);

            Vector3 pos = Vector3.zero;
            pos.y = Random.Range(-2.5f, 2.5f);
            go.transform.localPosition = pos;
        };

        antivirusSpawnTimer.onFinish += () =>
        {
            antivirusSpawnTimer.SetTimer(Random.Range(0.75f, 1.25f));
            Instantiate(antiVirus, new(15, Random.Range(3.5f, -3.5f), 0), transform.rotation);
        };
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        computerSpawnTimer.Update(delta);
        antivirusSpawnTimer.Update(delta);
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreboard.text = "Computers Phished: " + score;
    }

    public void DecreaseScore()
    {
        score -= 5;
        scoreboard.text = "Computers Phished: " + score;
    }
}
