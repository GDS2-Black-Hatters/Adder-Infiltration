using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] GameObject computerParent;

    GameObject laptop;
    // int laptopTexture;
    // public Texture whiteLaptop;
    // public Texture blackLaptop;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //computerParent = GameObject.Find("ComputerParent");
        //laptop = gameObject.transform.Find("LaptopModel").gameObject;
        /*
        laptopTexture = Random.Range(1, 2);
        if (laptopTexture == 1)
        {
            laptop.GetComponent<Material>().SetTexture("WhiteLaptop", whiteLaptop);
        }
        if (laptopTexture == 2)
        {
            laptop.GetComponent<Material>().SetTexture("BlackLaptop", blackLaptop);
        } */
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

        if (gameObject.transform.position.y >= 3.5f || gameObject.transform.position.y <= -3.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }
}
