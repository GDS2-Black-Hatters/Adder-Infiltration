using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVirusController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Transform cameraAnchor;
    private Rigidbody rb;
    private InputAction moveAction;

    private void Awake()
    {
        GameManager.LevelManager.SetPlayer(transform);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(InputManager.ControlScheme.MainGame);
        moveAction = inputManager.GetAction(InputManager.Controls.Move);
    }

    private void FixedUpdate()
    {
        MoveFixedUpdate();
    }

    private void OnEnable()
    {
        //Retrieve InputManager and register input events
        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(InputManager.ControlScheme.MainGame);

        inputManager.GetAction(InputManager.Controls.Move).canceled += MovementHalt;
    }

    private void OnDisable()
    {
        //Unregister input events from InputManager
        InputManager inputManager = GameManager.InputManager;
        if (!inputManager)
        {
            return;
        }
        inputManager.GetAction(InputManager.Controls.Move).canceled -= MovementHalt;
    }

    //Handle movement by force in fixed update so lag doesn't change the player's speed
    private void MoveFixedUpdate()
    {
        Vector2 moveDeltaV2 = moveAction.ReadValue<Vector2>();
        Vector3 xAxis = cameraAnchor.right;
        Vector3 forward = Vector3.Cross(xAxis, Vector3.up);
        Vector3 direction = (xAxis * moveDeltaV2.x) + (forward * moveDeltaV2.y);
        rb.AddForce(movementSpeed * direction, ForceMode.Acceleration);
        //rb.velocity = direction * MovementSpeed;
    }

    //Sets movement to zero when input is removed
    private void MovementHalt(InputAction.CallbackContext MoveDelta)
    {
        //rb.velocity = Vector3.zero;
    }
}
