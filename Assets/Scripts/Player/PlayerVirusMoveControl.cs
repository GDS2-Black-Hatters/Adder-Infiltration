using UnityEngine;

public class PlayerVirusMoveControl : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Transform cameraAnchor;

    private Rigidbody rb;
    private Vector2 movementDeltaV2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameManager.LevelManager.ActiveSceneController.Player.VirusController.onMovementInputUpdate += UpdateMovementInput;
    }

    private void UpdateMovementInput(Vector2 newDelta)
    {
        movementDeltaV2 = newDelta;
    }

    private void FixedUpdate()
    {
        MoveFixedUpdate();
    }

    private void MoveFixedUpdate()
    {
        Movement(movementDeltaV2, movementSpeed);
    }

    private void Movement(Vector2 movementDelta, float speed)
    {
        Vector3 xAxis = cameraAnchor.right;
        Vector3 forward = Vector3.Cross(xAxis, Vector3.up);
        Vector3 direction = (xAxis * movementDelta.x) + (forward * movementDelta.y);
        rb.AddForce(speed * direction, ForceMode.Acceleration);
    }

    public void Dash(float strength)
    {
        Movement(movementDeltaV2 == Vector2.zero ? Vector2.up : movementDeltaV2, strength);
    }
}
