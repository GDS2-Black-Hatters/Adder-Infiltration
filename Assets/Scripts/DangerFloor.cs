using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerFloor : MonoBehaviour
{
    // On trigger enter, damage player (and slow?)
    // On trigger exit, stop doing that

    private PlayerVirusMoveControl pvmc;
    float originalSpeed = 55f, slowSpeed = 20f;

    private void Start() {
       pvmc =  GameObject.Find("PlayerVirus").GetComponent<PlayerVirusMoveControl>();
    }

    private void OnTriggerEnter(Collider other) {
       if(other.tag == "Player")
                pvmc.UpdateSpeed(slowSpeed);
    }

    private void OnTriggerExit(Collider other) {
       if(other.tag == "Player")
                pvmc.UpdateSpeed(originalSpeed);
    }
}
