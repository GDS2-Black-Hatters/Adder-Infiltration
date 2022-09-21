using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerDetector : DetectorEnvironmentObject
{
    [System.Flags]
    private enum SpinDirection
    { none = 0, clockwise = 1, anticlockwise = 2, both = clockwise|anticlockwise}

    [SerializeField] private bool randomizeStartingRotation = true;
    [SerializeField] private SpinDirection allowedSpinDirection = SpinDirection.both;
    [SerializeField] private float rotationSpeed = 0.085f;
    [SerializeField] private Transform spinnerHeadTransform;

    protected override void Start()
    {
        base.Start();

        if(randomizeStartingRotation)
        {
            spinnerHeadTransform.Rotate(spinnerHeadTransform.up, Random.Range(0f, 360f));
        }

        if(allowedSpinDirection != SpinDirection.none)
        {
            Rotate roter = spinnerHeadTransform.gameObject.AddComponent<Rotate>();

            List<SpinDirection> availableDirection = new();
            //I refuse to believe this is what I have to do.... there's surely some trick with these binary stuff that I'm just not using..
            if((allowedSpinDirection & SpinDirection.clockwise) != 0)
            {
                availableDirection.Add(SpinDirection.clockwise);
            }
            if((allowedSpinDirection & SpinDirection.anticlockwise) != 0)
            {
                availableDirection.Add(SpinDirection.anticlockwise);
            }

            SpinDirection dir = availableDirection[Random.Range( 0, availableDirection.Count)];

            switch (dir)
            {
                case SpinDirection.clockwise:
                roter.rotationSpeed = rotationSpeed * Vector3.up;
                break;

                case SpinDirection.anticlockwise:
                roter.rotationSpeed = rotationSpeed * Vector3.down;
                break;

                default:
                Debug.LogError("No available spin direction found, destroying the Rotate Monobehaviour");
                Destroy(roter);
                break;
            }
        }
    }

    public override void PlayerInRange()
    {
        OnDetect.Invoke();
    }
}
