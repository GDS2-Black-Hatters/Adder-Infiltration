using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareAndSpin : MonoBehaviour
{
    [Header("Stare")]
    [SerializeField] public float rotateToStareMaxSpeed = 300;
    [Range(0,1)] [SerializeField] public float stareAccuracyRequirement = 0.95f;
    [System.NonSerialized] public Transform stareTarget;

    [Header("Spin")]
    [SerializeField] public float spinSpeed = 200;

    private void Awake()
    {
        this.enabled = stareTarget != null;
    }

    private void Update()
    {
        Vector3 stareDirection = (stareTarget.position - transform.position).normalized;
        // Debug.DrawRay(transform.position, transform.forward, Color.blue, 1);
        // Debug.DrawRay(transform.position, stareDirection, Color.red, 1);
        // Debug.Log(Vector3.Dot(transform.forward, stareDirection));
        if(Vector3.Dot(transform.forward, stareDirection) < stareAccuracyRequirement)
            Stare(stareDirection);
        Spin(stareDirection);
    }

    private void Stare()
    {
        Stare(stareTarget.position - transform.position);
    }

    private void Stare(Vector3 stareDirection)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stareDirection, Vector3.up), rotateToStareMaxSpeed * Time.deltaTime);
    }

    private void Spin()
    {
        Spin(stareTarget.position - transform.position);
    }

    private void Spin(Vector3 axis)
    {
        transform.RotateAround(transform.position, axis, spinSpeed * Time.deltaTime);
    }
}
