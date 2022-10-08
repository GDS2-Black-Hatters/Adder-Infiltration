using UnityEngine;
using UnityEngine.InputSystem;
using static LevelManager;
using static InputManager;
using static ActionInputSubscriber.CallbackContext;

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

    private void Start()
    {
        GameManager.LevelManager.player.virusController.onLookInputUpdate += RotateCamera;
    }

    private void Update()
    {
        Vector3 boomedPosition = transform.position;
        TranslateBoom(ref boomedPosition, transform.up, upDistance);
        TranslateBoom(ref boomedPosition, -leftRightRotTransform.forward, backDistance);
        cameraBindTransform.position = boomedPosition;
    }

    private void TranslateBoom(ref Vector3 boomV3, Vector3 boomDirection, float boomDistance)
    {
        if (Physics.SphereCast(boomV3, boomSphereRadius, boomDirection, out RaycastHit boomResult, boomDistance, sphereCastMask))
        {
            boomV3 += (boomResult.distance - boomSphereRadius) * boomDirection;
        }
        else
        {
            boomV3 += boomDistance * boomDirection;
        }
    }

    private void RotateCamera(Vector2 lookDeltaV2)
    {
        RotateLeftRight(lookDeltaV2.x);
        LookUpDown(lookDeltaV2.y);
    }

    private void RotateLeftRight(float deltaAngle)
    {
        leftRightRotTransform.Rotate(leftRightRotTransform.up, deltaAngle);
    }

    private void LookUpDown(float delta)
    {
        vertOffsetLerpValue = Mathf.Clamp01(vertOffsetLerpValue + delta);
        upDownRotTransform.localRotation = Quaternion.Euler(Mathf.LerpAngle(lowerClampAngle, -upperClampAngle, vertOffsetLerpValue), 0, 0);
    }
}
