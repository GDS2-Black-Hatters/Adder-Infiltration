using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRepeater : MonoBehaviour
{
    private enum Axis { x, y, z }

    [SerializeField] private Axis loopAlongAxis;
    [SerializeField] private Transform warpAlongTransform;
    [SerializeField] private bool autoGetChildrenOnStart = false;
    [SerializeField] private Transform[] loopObjects;

    [SerializeField] private float objectSeparation = 0.5f;

    private float maxDist;

    private Vector3 axisModifier;

    private void Start()
    {
        switch (loopAlongAxis)
        {
            case Axis.x:
            default:
            axisModifier = Vector3.right;
            break;

            case Axis.y:
            axisModifier = Vector3.up;
            break;

            case Axis.z:
            axisModifier = Vector3.forward;
            break;
        }

        if(autoGetChildrenOnStart)
        {
            loopObjects = new Transform[transform.childCount];
            for(int i = 0; i < transform.childCount; i++)
            {
                loopObjects[i] = transform.GetChild(i);
            }
        }

        maxDist = objectSeparation * (loopObjects.Length - 1) * 0.5f;

        for (int i = 0; i < loopObjects.Length; i++)
        {
            loopObjects[i].localPosition = warpAlongTransform.position + axisModifier * (-maxDist + i * objectSeparation);
        }
    }

    private void Update()
    {
        Vector3 deltaVect = axisModifier * 2 * maxDist;

        foreach(Transform loopObj in loopObjects)
        {
            while( axisDifference(warpAlongTransform.position, loopObj.position, loopAlongAxis) < -maxDist)
            {
                loopObj.position += deltaVect;
            }
            while( axisDifference(warpAlongTransform.position, loopObj.position, loopAlongAxis) > maxDist)
            {
                loopObj.position -= deltaVect;                    
            }
        }
    }

    private float axisDifference(Vector3 a, Vector3 b, Axis axis)
    {
        switch (axis)
        {
            case Axis.x:
            default:
            return b.x - a.x;

            case Axis.y:
            return b.y - a.y;

            case Axis.z:
            return b.z - a.z;
        }
    }
}
