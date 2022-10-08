using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && gameObject.transform.position.y <= 4)
        {
            gameObject.transform.position = new Vector3(-5, gameObject.transform.position.y + moveSpeed * Time.deltaTime, 0);
        }
        else if (gameObject.transform.position.y >= -4)
        {
            gameObject.transform.position = new Vector3(-5, gameObject.transform.position.y - moveSpeed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Computer" && transform.childCount < 1)
        {
            other.transform.parent = gameObject.transform;
            other.transform.position = transform.position;
            other.attachedRigidbody.velocity = new Vector3(0, 0, 0);
        }

        if (other.tag == "Antivirus")
        {
            GameObject.Find("ComputerParent").GetComponent<ComputerParent>().DecreaseScore();
        }
    }
}
