using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoomCameraControl : MonoBehaviour
{
    [SerializeField] private Transform cameraBindTransform;

    private float vertOffsetLerpValue = 0.5f;

    [Header("Boom Setting")]
    [SerializeField] private float boomSphereRadius;
    [SerializeField] private LayerMask sphereCastMask;

    [Header("Vertical")]
    [SerializeField] private float upMinDistance = 2;
    [SerializeField] private float upMaxDistance = 5;
    private float upDistance
    {
        get { return Mathf.Lerp(upMinDistance, upMaxDistance, vertOffsetLerpValue); }
    }
    [SerializeField] private float lowerClampAngle = 70;
    [SerializeField] private float upperClampAngle = 85;
    [SerializeField] private Transform upDownRotTransform;


    [Header("Horizontal")]
    [SerializeField] private float backMinDistance = 2;
    [SerializeField] private float backMaxDistance = 6;
    private float backDistance
    {
        get { return Mathf.Lerp(backMinDistance, backMaxDistance, vertOffsetLerpValue); }
    }
    [SerializeField] private Transform leftRightRotTransform;


    private void Update()
    {
        Vector3 boomedPosition = transform.position;
        TranslateBoom(ref boomedPosition, transform.up, upDistance);
        TranslateBoom(ref boomedPosition, -transform.forward, backDistance);
        cameraBindTransform.position = boomedPosition;
    }

    private void TranslateBoom(ref Vector3 boomV3, Vector3 boomDirection, float boomDistance)
    {
        RaycastHit boomResult;
        if(Physics.SphereCast(boomV3, boomSphereRadius, boomDirection, out boomResult, boomDistance, sphereCastMask))
        {
            boomV3 += (boomResult.distance - boomSphereRadius) * boomDirection;
        }
        else
        {
            boomV3 += boomDistance * boomDirection;
        }
    }

    private void OnEnable()
    {
        //Retrieve InputManager and register input events
        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(InputManager.ControlScheme.MainGame);

        inputManager.GetAction(InputManager.Controls.Look).performed += RotateCamera;
    }

    private void OnDisable()
    {
        InputManager inputManager = GameManager.InputManager;
        if (!inputManager)
        {
            return;
        }
        inputManager.GetAction(InputManager.Controls.Look).performed -= RotateCamera;
    }

    private void RotateCamera(InputAction.CallbackContext LookDelta)
    {
        Vector2 lookDeltaV2 = LookDelta.ReadValue<Vector2>();
        RotateLeftRight(lookDeltaV2.x);
        LookUpDown(lookDeltaV2.y);
    }

    private void RotateLeftRight(float deltaAngle)
    {
        leftRightRotTransform.Rotate(Vector3.up, deltaAngle);
    }

    private void LookUpDown(float delta)
    {
        vertOffsetLerpValue = Mathf.Clamp01(vertOffsetLerpValue + delta);
        upDownRotTransform.localRotation = Quaternion.Euler(Mathf.LerpAngle(lowerClampAngle, -upperClampAngle, vertOffsetLerpValue), 0, 0);
    }
}
