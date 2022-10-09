using UnityEngine;

public class Hook : MonoBehaviour
{
    [field: SerializeField] public MinigameController MinigameController { get; private set; }
    [SerializeField] private float moveSpeed = 5;
    private Rigidbody2D rb;
    public Computer victim { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float movement = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) ? moveSpeed : -moveSpeed;
        rb.velocity = new(0, movement);
    }

    public void SetVictim(Computer newVictim)
    {
        victim = newVictim;
    }
}
