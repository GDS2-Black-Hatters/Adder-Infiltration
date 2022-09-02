using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10;
    public float rotationSpeed{
        get{
            return _rotationSpeed;
        }
        set{
            _rotationSpeed = value;
            this.enabled = (_rotationSpeed != 0);
        }
    }
    [SerializeField] private Vector3 axisChangeFrequency = Vector3.one;
    [System.NonSerialized] public Vector3 axisOffset;

    void Awake()
    {
        axisOffset = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
    }

    void Update()
    {
        transform.Rotate(GetRotationAxis(), _rotationSpeed);
    }

    Vector3 GetRotationAxis()
    {
        return new Vector3(Mathf.Sin(Time.time * axisChangeFrequency.x + axisOffset.x),  Mathf.Sin(Time.time * axisChangeFrequency.y + axisOffset.y), Mathf.Sin(Time.time * axisChangeFrequency.z + axisOffset.z)).normalized;
    }
}
