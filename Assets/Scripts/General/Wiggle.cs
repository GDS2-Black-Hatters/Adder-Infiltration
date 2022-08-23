using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 randomPerlinOffset;

    public float WiggleMaxDistance = 0.1f;
    public float WiggleFrequency = 1;

    private void Start() 
    {
        initialPosition = transform.localPosition;
        
        randomPerlinOffset = new Vector3(Random.value, Random.value, Random.value);
    }

    void Update()
    {
        transform.localPosition = initialPosition + new Vector3(Mathf.PerlinNoise(Time.time * WiggleFrequency, randomPerlinOffset.x) - 0.5f, Mathf.PerlinNoise(Time.time * WiggleFrequency, randomPerlinOffset.y) - 0.5f, Mathf.PerlinNoise(Time.time * WiggleFrequency, randomPerlinOffset.z) - 0.5f) * WiggleMaxDistance;
    }
}
