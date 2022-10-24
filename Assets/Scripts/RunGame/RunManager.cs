using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunManager: MonoBehaviour
{
    Rigidbody obstacle;

    float startTimer = 0.2f;
    float maxTimer;
    float fullTimer = 5;
    float timer;
    float distanceRan;
    [SerializeField] private GameObject runner;
    [SerializeField] private TextMeshProUGUI gameOver;
    [SerializeField] private TextMeshProUGUI scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        obstacle = gameObject.GetComponentInChildren<Rigidbody>();
        maxTimer = fullTimer;
        timer = fullTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        distanceRan += Time.deltaTime * 10 / CalculateTimer();
        if (distanceRan <= 1000)
        {
            scoreboard.text = "Distance Ran: " + (int)distanceRan + "m";
        }
        else
        {
            scoreboard.text = "Distance Ran: " + (distanceRan / 1000).ToString("F2") + "km";
        }

        if (timer <= 0)
        {
            SpawnObstacle();
            if (maxTimer > 1)
            {
                maxTimer -= 0.5f;
            }
            timer = maxTimer;
        }
        // this is needed because for the first two frames the runner isnt visible
        if (startTimer >= 0)
        {
            startTimer -= Time.deltaTime;
        }
        // if the runner isnt rendered (behind the camera) the game ends
        if (runner.GetComponent<Renderer>().isVisible == false && startTimer <= 0)
        {
            gameOver.text = "GAME OVER";
            EndGame();
        }

        // purely for testing dw about it
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnObstacle();
        }
    }

    float CalculateTimer()
    {
        return maxTimer / fullTimer;
    }

    void SpawnObstacle()
    {
        Rigidbody obstacleRb;
        Vector3 spawnPoint = new Vector3((Random.Range(-9, 9)), Random.Range(0, 3), gameObject.transform.position.z);
        obstacleRb = Instantiate(obstacle, spawnPoint, gameObject.transform.rotation);
        // the original object was having some collision issues so collision is disabled by default
        obstacleRb.GetComponent<Collider>().isTrigger = false;
        //makes the box a random size on x and y axis within range.
        obstacleRb.GetComponent<Transform>().localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), 1);
        // ensures that the boxes glide just above the floor and can collide with the runner no problem.
        obstacleRb.GetComponent<Transform>().position = new Vector3(obstacleRb.GetComponent<Transform>().position.x, (obstacleRb.GetComponent<Transform>().localScale.y / 2) + 1, obstacleRb.GetComponent<Transform>().position.z);
        // increases obstacle speed with time
        obstacleRb.velocity = new Vector3(0, 0, -10 / CalculateTimer());
    }

    void EndGame()
    {
        //idk make it do something :)
    }
}
