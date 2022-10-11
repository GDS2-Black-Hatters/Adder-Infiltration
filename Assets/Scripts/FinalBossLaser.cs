using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossLaser : MonoBehaviour
{

    public Transform laserOrigin, playerTransform;
    
    public LineRenderer laserLineR, warningLaser;
    public bool shootingBossLaser = false;

    // Start is called before the first frame update
    void Awake()
    {
        laserLineR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        laserLineR.SetPosition(0, laserOrigin.transform.position);
        warningLaser.SetPosition(0, laserOrigin.transform.position);


        warningLaser.SetPosition(1, playerTransform.transform.position);
       // Debug.Log("A" + playerTransform.transform.position);

       if(shootingBossLaser)
       {
        RaycastHit hit;
        if(Physics.Raycast(laserOrigin.transform.position, playerTransform.transform.position - laserOrigin.transform.position, out hit, Mathf.Infinity))
        {
            laserLineR.SetPosition(1, hit.point);
        }
       } else {
            laserLineR.SetPosition(1, laserOrigin.transform.position);
       }
    }
}
