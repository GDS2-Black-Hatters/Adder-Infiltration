using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 4)
        {
            GameObject.Find("ComputerParent").GetComponent<ComputerParent>().UpdateScoreBoard();
            Destroy(gameObject);
        }

            if (transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }
}
