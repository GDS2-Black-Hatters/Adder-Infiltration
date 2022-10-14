using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVirusMoveControl : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Transform cameraAnchor;

    private Rigidbody rb;
    private Vector2 movementDeltaV2;

    public void UpdateSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameManager.LevelManager.player.virusController.onMovementInputUpdate += UpdateMovementInput;
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
        Vector3 xAxis = cameraAnchor.right;
        Vector3 forward = Vector3.Cross(xAxis, Vector3.up);
        Vector3 direction = (xAxis * movementDeltaV2.x) + (forward * movementDeltaV2.y);
        rb.AddForce(movementSpeed * direction, ForceMode.Acceleration);
    }
}
