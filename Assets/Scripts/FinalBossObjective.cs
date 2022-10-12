using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalBossObjective : MonoBehaviour
{
    private bool playerIn = false;
    public bool complete = false;

    public Transform finalBossTf, laserOriginPosition;

    private LineRenderer lr;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, laserOriginPosition.transform.position);
        lr.SetPosition(1, finalBossTf.transform.position);
        lr.enabled = false;
    }

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

        progressText.text = System.Math.Round(progress/800 * 100) + "%";

        lr.enabled = complete;

    }

    public void ResetProgress()
    {
        progress = 0;
        complete = false;
    }
}
