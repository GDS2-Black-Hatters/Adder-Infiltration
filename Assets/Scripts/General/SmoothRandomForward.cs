using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRandomForward : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    public float rotSpeed;

    private float xRotChangeFrequency;
    private float yRotChangeFrequency;
    private float zRotChangeFrequency;

    private void Awake()
    {
        xRotChangeFrequency = Random.Range(0.1f,1);
        yRotChangeFrequency = Random.Range(0.1f,1);
        zRotChangeFrequency = Random.Range(0.1f,1);
    }

    void Update()
    {
        float xRotRate = Mathf.Sin(xRotChangeFrequency * Time.time);
        float yRotRate = Mathf.Sin(yRotChangeFrequency * Time.time);
        float zRotRate = Mathf.Sin(zRotChangeFrequency * Time.time);

        transform.Rotate(xRotRate * rotSpeed * Time.deltaTime, yRotRate * rotSpeed * Time.deltaTime, zRotRate * rotSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);        
    }
}
