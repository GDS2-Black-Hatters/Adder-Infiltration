using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossRotate : MonoBehaviour
{
    public Transform bossModel;

    // Update is called once per frame
    void FixedUpdate()
    {
        bossModel.transform.Rotate(0,20*Time.deltaTime,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Debug.Log("PLAYER TOUCHED BOSS -> KILL EVENT / GAME OVER");
        }
    }
}
