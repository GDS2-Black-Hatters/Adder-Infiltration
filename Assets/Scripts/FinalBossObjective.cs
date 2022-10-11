using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalBossObjective : MonoBehaviour
{
    private bool playerIn = false;
    public bool complete = false;

    [SerializeField]
    private float progress = 0;

    [SerializeField]
    private TextMeshPro progressText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            playerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            playerIn = false;
    }

    private void Update()
    {
        if(playerIn && progress < 800)
            progress++;
        if(progress >= 800)
            complete = true;

        //progressText.text = System.Math.Round(progress/800 * 100) + "%";
    }

    public void ResetProgress()
    {
        progress = 0;
        complete = false;
    }
}
