using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseRange : MonoBehaviour
{
    public bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange == true)
        {
            Debug.Log("playerInRange = true");
        }
        else
        {
            Debug.Log("playerInRange = false");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
