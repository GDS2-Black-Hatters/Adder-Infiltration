using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    Rigidbody rb;
    bool onFloor = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(onFloor);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onFloor)
            {
                rb.velocity += new Vector3(0, 10, 0);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - (5 * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + (5 * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
        }
    }
}
