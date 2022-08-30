using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerVirusController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float cameraSensitivity = 0.5f;
    [SerializeField] private Transform cameraAnchor;
    private Rigidbody rb;
    private InputAction moveAction;

    public Slider healthSlider;
    private float health;
    private float maxHealth = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(InputManager.ControlScheme.MainGame);
        moveAction = inputManager.GetAction(InputManager.Controls.Move);
        GameManager.LevelManager.SetPlayer(transform);

        health = maxHealth;
        healthSlider = gameObject.transform.Find("Canvas").gameObject.transform.Find("HealthBar").gameObject.GetComponent<Slider>();
        healthSlider.value = CalculateHealth();
    }

    private void FixedUpdate()
    {
        MoveFixedUpdate();

        if (health <= 0)
        {
            Debug.Log("Death");
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

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    //decreases health by a given input
    public void DecreaseHealth(int amount)
    {
        health -= amount;
        healthSlider.value = CalculateHealth();
    }
}
