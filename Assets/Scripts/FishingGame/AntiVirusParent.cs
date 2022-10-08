using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntiVirusParent : MonoBehaviour
{
    private float spawnTimer = 0;
    private float spawnNextAnti = 1;

    private Rigidbody antiVirus;

    private TextMeshProUGUI scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        antiVirus = transform.Find("AntiVirus").GetComponent<Rigidbody>();
        scoreboard = GameObject.Find("Score count").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnNextAnti)
        {
            Rigidbody antivirus;
            antivirus = Instantiate(antiVirus, new Vector3(15, Random.Range(3.5f, -3.5f), 0), transform.rotation);
            antivirus.velocity = new Vector3(Random.Range(-10f,-15f), 0, 0);
            spawnNextAnti = Random.Range(0.75f, 1.25f);
            spawnTimer = 0;
        }
    }
}

