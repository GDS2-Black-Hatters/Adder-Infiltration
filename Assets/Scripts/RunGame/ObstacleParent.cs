using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObstacleParent : MonoBehaviour
{
    Rigidbody obstacle;
    

    float maxTimer;
    float fullTimer = 5;
    float timer;
    float distanceRan;

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

        obstacleRb.GetComponent<Collider>().isTrigger = false;
        obstacleRb.GetComponent<Transform>().localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), 1);
        obstacleRb.GetComponent<Transform>().position = new Vector3(obstacleRb.GetComponent<Transform>().position.x, (obstacleRb.GetComponent<Transform>().localScale.y / 2) + 1, obstacleRb.GetComponent<Transform>().position.z);
        obstacleRb.velocity = new Vector3(0, 0, -10 / CalculateTimer());
    }
}
