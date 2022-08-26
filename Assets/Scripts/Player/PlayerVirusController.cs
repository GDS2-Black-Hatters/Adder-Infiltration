using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVirusController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float cameraSensitivity = 0.5f;
    [SerializeField] private Transform cameraAnchor;
    private Rigidbody rb;
    private InputAction moveAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(InputManager.ControlScheme.MainGame);
        moveAction = inputManager.GetAction(InputManager.Controls.Move);
    }

    private void Update()
    {
        if (moveAction.IsPressed())
        {
            Move();
        }
    }

    private void OnEnable()
    {
        //Retrieve InputManager and register input events
        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(InputManager.ControlScheme.MainGame);

        inputManager.GetAction(InputManager.Controls.Look).performed += RotateCamera;
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
        inputManager.GetAction(InputManager.Controls.Look).performed -= RotateCamera;
        inputManager.GetAction(InputManager.Controls.Move).canceled -= MovementHalt;
    }

    private void RotateCamera(InputAction.CallbackContext LookDelta)
    {
        Vector2 lookDeltaV2 = LookDelta.ReadValue<Vector2>() * cameraSensitivity;
        Vector2 rot = cameraAnchor.eulerAngles;
        rot.x = Mathf.Clamp(rot.x - lookDeltaV2.y - (rot.x > 90 ? 360 : 0), -90, 90);
        rot.y += lookDeltaV2.x;
        cameraAnchor.eulerAngles = rot;
    }

    //Updates movement direction according to input recieved
    //actual movement is handled in Update()
    private void Move()
    {
        Vector2 moveDeltaV2 = moveAction.ReadValue<Vector2>();
        Vector3 xAxis = cameraAnchor.right;
        Vector3 forward = Vector3.Cross(xAxis, Vector3.up);
        Vector3 direction = (xAxis * moveDeltaV2.x) + (forward * moveDeltaV2.y);
        rb.AddForce(MovementSpeed * direction, ForceMode.Acceleration);
        //rb.velocity = direction * MovementSpeed;
    }

    //Sets movement to zero when input is removed
    private void MovementHalt(InputAction.CallbackContext MoveDelta)
    {
        //rb.velocity = Vector3.zero;
    }
}
