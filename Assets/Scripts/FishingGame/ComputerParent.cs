using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;



public class ComputerParent : MonoBehaviour
{
    public float spawnTimer = 0;

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
        if (spawnTimer >= 1)
        {
            Rigidbody target;
            target = Instantiate(computer, new Vector3(15,Random.Range(3.5f,-3.5f),0), transform.rotation);
            target.velocity = new Vector3(-10, 0, 0);
            spawnTimer = 0;
        }
    }

    public void UpdateScoreBoard()
    {
        score++;
        scoreboard.text = "Computers Phished: " + score;
    }
}
