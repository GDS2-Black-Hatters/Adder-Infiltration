using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoverAtHeight : MonoBehaviour
{
    [Header("Hover Settings")]
    [SerializeField] private float hoverHeight = 0.5f;
    [SerializeField] private float hoverForce = 5f;
    [SerializeField] private LayerMask hoverPhysicsMask;

    [Header("BobbleSettings")]
    [SerializeField] private float bobbleMaxAmplitude = 0.2f;
    [SerializeField] private float bobbleFrequency = 1f;
    private float noiseSeed;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        noiseSeed = Random.value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float tickHoverHeight = hoverHeight + bobbleMaxAmplitude * 0.5f * (Mathf.PerlinNoise(Time.time * bobbleFrequency, noiseSeed) - 0.5f);

        RaycastHit downFireRayInfo;
        if(!Physics.Raycast(transform.position, Vector3.down, out downFireRayInfo, tickHoverHeight * 1.5f, hoverPhysicsMask.value))
        {
            //set ray distance to sth higher than hover height if the ray hit nothing
            downFireRayInfo.distance = tickHoverHeight * 1.5f;
        }
        Debug.Log(downFireRayInfo.distance);
        rb.AddForce((downFireRayInfo.distance - tickHoverHeight) * hoverForce * Vector3.down, ForceMode.Acceleration);
    }
}
