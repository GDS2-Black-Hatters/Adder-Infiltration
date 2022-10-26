using TMPro;
using UnityEngine;

public class RunGameplayScreen : MinigameGameplayScreen
{
    //This is a class I created for timing.
    //Its purpose is to keep track of the time for you.
    //All you pass it how long the timer goes for.
    //Then update it in the update method by passing in deltatime.
    private readonly TimeTracker spawnTimer = new(2);

    public float distanceRan { get; private set; } = 0;
    [SerializeField] private int maxSpawns = 10;
    [SerializeField] private GameObject runner;
    [SerializeField] private Obstacle obstaclePrefab;

    protected override void OnEnable()
    {
        distanceRan = 0;
        base.OnEnable();
    }

    private void Start()
    {
        //The timer will automatically call a delegate method when it finishes.
        //We can simply bind a method to that delegate method.
        //In this case I created an anonymouse method (better known as a lambda)
        //and assigned it to the delegate method.
        spawnTimer.onFinish += () =>
        {
            //Fun fact: Multiplication is 10x faster than division. Use multiplication whenever you can.
            int numSpawns = Mathf.Clamp((int)(distanceRan * 0.1) + 1, 1, maxSpawns);
            for (int i = 0; i < numSpawns; i++)
            {
                //The 100 offset is for Hub reasons.
                //Basically without it, the minigames will overlap each other's camera in the hub
                //Because they are in the same position and such.
                Vector3 spawnPoint = new(100 + Random.Range(-9, 9), Random.Range(0, 3), 50);
                Transform obstacle = Instantiate(obstaclePrefab, spawnPoint, transform.rotation).transform;
                obstacle.parent = transform;
                removables.Add(obstacle.gameObject);
            }

            float timer = Mathf.Clamp(spawnTimer.timer - 0.02f, 0.5f, spawnTimer.timer);

            //As a hidden feature of the class. Since the timer only calls the
            //delegate once and only when it is finished, if you were to reset it
            //on it delegate, you get a looping timer.
            //In this case, either calling SetTimer() to set to a different timer
            //to tick down from. Or calling Reset() will make the timer loop itself.
            spawnTimer.SetTimer(timer);
        };
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        spawnTimer.Update(delta);
        distanceRan += delta * 0.3f;
        UpdateScore();
    }

    protected override void UpdateScore()
    {
        //$ in front of the string is interpolation. tl:dr it just reads easier than adding everything.
        scoreboard.text = $"Distance Ran: {distanceRan:F2}km";
    }
}
