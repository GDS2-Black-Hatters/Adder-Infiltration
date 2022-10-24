using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandRotateArc : MonoBehaviour
{
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private float rotateSpeed = 20;
    [SerializeField] private Vector2 rotationClampAngle = new(-60,60);
    
    private float targetAngle;
    private float prevAngle = 0;
    private float overDiff;
    private float t;

    private void Start()
    {
        targetAngle = Random.Range(rotationClampAngle.x, rotationClampAngle.y);
        overDiff = 1/Mathf.Abs(prevAngle - targetAngle);
        t = 0;
    }

    private void Update()
    {
        if(t >= 1)
        {
            prevAngle = targetAngle;
            targetAngle = Random.Range(rotationClampAngle.x, rotationClampAngle.y);
            overDiff = 1/Mathf.Abs(prevAngle - targetAngle);
            t = 0;
        }
        t += rotateSpeed * overDiff * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(Mathf.SmoothStep(prevAngle, targetAngle, t), rotateAxis);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        int arcResolution = Mathf.CeilToInt(rotationClampAngle.y - rotationClampAngle.x);
        Vector3 pointX, pointY = transform.TransformPoint(Quaternion.AngleAxis(rotationClampAngle.x, rotateAxis) * Vector3.forward);
        for(int x = 0; x < arcResolution; x++)
        {
            pointX = pointY;
            pointY = transform.TransformPoint(Quaternion.AngleAxis(rotationClampAngle.x + (rotationClampAngle.y - rotationClampAngle.x)/arcResolution * x, rotateAxis) * Vector3.forward);
            Gizmos.DrawLine(pointX, pointY);
        }
    }
}
