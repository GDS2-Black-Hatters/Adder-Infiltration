using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private RunGameplayScreen runManager;
    private Rigidbody rb;
    private bool onFloor = true;
    [SerializeField] private float speed = 5; //The speed of the runner (when going left or right).
    private float moveMult;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (onFloor)
            {
                rb.velocity += new Vector3(0, 10, 0);
            }
        }

        //This is a default key binding in the Input Manager Settings page for the project.
        //This is the same with the jump button earlier in the code.
        //You should be able to understand this if you did the key bindings in UE4.
        moveMult = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate() //Any physics movement should be in fixed update.
    {
        //Grab the velocity of the rigidbody.
        //You grab a copy of this to save the pain of having to type the same value
        //for every value when you just want to modify one field, like the x axis.
        Vector3 vel = rb.velocity;

        //The x velocity determines the direction the gameobject goes.
        //If x is negative, left else right.
        vel.x = speed * moveMult;

        //Again, we only modified x, the rest stays the same.
        rb.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision) //Nice job with this.
    {
        //Use the CompareTag instead of tag == "Floor" way.
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }

    private void OnBecameInvisible()
    {
        GameOver();
    }

    public void GameOver()
    {
        runManager.GameOver();
    }
}
