using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    GameObject computerParent;

    // Start is called before the first frame update
    void Start()
    {
        computerParent = GameObject.Find("ComputerParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 4)
        {
            computerParent.GetComponent<ComputerParent>().IncreaseScore();
            Destroy(gameObject);
        }

        if (transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }
}
