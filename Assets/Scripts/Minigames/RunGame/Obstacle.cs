using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -10);

        transform.localScale = new Vector3(Random.Range(2, 5), Random.Range(2, 5), 1);
        Vector3 pos = transform.position;
        pos.y = (transform.localScale.y / 2) + 1;
        transform.position = pos;
    }

    //Yeah, there is an inbuilt Unity method for when the gameobject
    //goes out of the camera's view.
    //NOTE: Keep in mind that the scene camera is counted as a camera
    //this method won't be called if the scene camera can see it even
    //though the game camera cannot see it.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Try get component attempts to get the component in a gameobject.
        //If it fails, it returns false, otherwise it will return true
        //It will also assign the out variable with the component it found.
        if (other.TryGetComponent(out Runner runner))
        {
            runner.GameOver();
        }
    }
}
