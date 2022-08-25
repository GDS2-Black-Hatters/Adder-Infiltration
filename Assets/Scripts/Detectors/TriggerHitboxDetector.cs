using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerHitboxDetector : BaseDetector
{
    private void Start()
    {
        LevelSceneController lsc = FindObjectOfType<LevelSceneController>();
        if(lsc)
            OnDetection += lsc.PlayerDetected;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerDetected();
    }
}
