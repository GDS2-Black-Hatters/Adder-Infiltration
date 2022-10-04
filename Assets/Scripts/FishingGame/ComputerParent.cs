using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;



public class ComputerParent : MonoBehaviour
{
    public float spawnTimer = 0;
    private float spawnNextComputer = 2;

    private Rigidbody computer;

    public int score = 0;
    private TextMeshProUGUI scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        computer = transform.Find("Computer").GetComponent<Rigidbody>();
        scoreboard = GameObject.Find("Score count").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnNextComputer)
        {
            Rigidbody target;
            target = Instantiate(computer, new Vector3(15,Random.Range(3.5f,-3.5f),0), Quaternion.Euler(0f,0f,0f));
            target.velocity = new Vector3(-10, Random.Range(-2.5f,2.5f), 0);
            spawnNextComputer = Random.Range(1.5f, 2.5f);
            spawnTimer = 0;
        }
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
