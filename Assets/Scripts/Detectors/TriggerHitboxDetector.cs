using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerHitboxDetector : BaseDetector
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerDetected();
    }
}
