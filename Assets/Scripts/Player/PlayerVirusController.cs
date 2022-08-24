using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVirusController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 0.5f;

    [SerializeField] private Transform CameraAnchor;

    private Vector3 movementV3 = Vector3.zero;

    private void Update()
    {
        transform.Translate(CameraAnchor.TransformVector(movementV3) * MovementSpeed);
    }

    private void OnEnable()
    {
        //Retrieve InputManager and register input events
        InputManager inputManager = GameManager.InputManager;
        inputManager.GetAction(InputManager.Controls.Look).performed += RotateCamera;
        inputManager.GetAction(InputManager.Controls.Move).performed += Move;
        inputManager.GetAction(InputManager.Controls.Move).canceled += MovementHalt;
    }
    
    private void OnDisable()
    {
        //Unregister input events from InputManager
        InputManager inputManager = GameManager.InputManager;
        inputManager.GetAction(InputManager.Controls.Look).performed -= RotateCamera;
        inputManager.GetAction(InputManager.Controls.Move).performed -= Move;
        inputManager.GetAction(InputManager.Controls.Move).canceled += MovementHalt; //+= ?
    }

    private void RotateCamera(InputAction.CallbackContext LookDelta)
    {
        Vector2 lookDeltaV2 = LookDelta.ReadValue<Vector2>();
        CameraAnchor.Rotate(Vector3.up, lookDeltaV2.x);
    }

    //Updates movement direction according to input recieved
    //actual movement is handled in Update()
    private void Move(InputAction.CallbackContext MoveDelta)
    {
        Vector2 moveDeltaV2 = MoveDelta.ReadValue<Vector2>();
        movementV3.x = moveDeltaV2.x;
        movementV3.z = moveDeltaV2.y;
    }

    //Sets movement to zero when input is removed
    private void MovementHalt(InputAction.CallbackContext MoveDelta)
    {
        movementV3 = Vector3.zero;
    }
}
