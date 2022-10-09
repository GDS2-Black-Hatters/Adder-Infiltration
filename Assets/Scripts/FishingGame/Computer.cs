using UnityEngine;

public class Computer : MonoBehaviour
{
    private MinigameController computerParent;
    private Rigidbody2D rb;
    private Hook follow;

    // Start is called before the first frame update
    private void Start()
    {
        computerParent = GetComponentInParent<MinigameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new(-10, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (follow)
        {
            transform.position = follow.transform.position;
        }

        if (transform.position.y >= 4)
        {
            computerParent.IncreaseScore();
            follow.SetVictim(null);
            Destroy(gameObject);
        }

        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hook hook) && !hook.victim)
        {
            follow = hook;
            follow.SetVictim(this);
        }
    }
}
