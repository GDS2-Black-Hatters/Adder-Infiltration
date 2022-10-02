using UnityEngine;
using static InputManager;

public class PlayerVirusController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Transform cameraAnchor;
    private Rigidbody rb;

    private void Awake()
    {
        GameManager.LevelManager.SetPlayer(transform);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveFixedUpdate();
    }

    //Handle movement by force in fixed update so lag doesn't change the player's speed
    private void MoveFixedUpdate()
    {
        Vector2 moveDeltaV2 = MainGameMove.ReadValue<Vector2>();
        Vector3 xAxis = cameraAnchor.right;
        Vector3 forward = Vector3.Cross(xAxis, Vector3.up);
        Vector3 direction = (xAxis * moveDeltaV2.x) + (forward * moveDeltaV2.y);
        rb.AddForce(movementSpeed * direction, ForceMode.Acceleration);
        //rb.velocity = direction * MovementSpeed;
    }
}
