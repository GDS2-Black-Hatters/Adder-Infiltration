using UnityEngine;

public class AntiVirus : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new(Random.Range(-10f, -15f), 0);
    }

    private void Update()
    {
        if (transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hook hook))
        {
            hook.MinigameController.DecreaseScore();
        }
    }
}
