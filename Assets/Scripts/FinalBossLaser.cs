using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossLaser : MonoBehaviour
{

    public Transform laserOrigin, playerTransform;
    
    public LineRenderer laserLineR, warningLaser;
    public bool shootingBossLaser = false;
    public Material warningLaserMat1,  warningLaserMat2,  warningLaserMat3;

    // Start is called before the first frame update
    void Awake()
    {
        laserLineR = GetComponent<LineRenderer>();
    }

    void AboutToShootLaser()
    {
        warningLaser.material = warningLaserMat2;
        // Play sound of a laser about to fire
        Debug.Log("Laser about to fire");
        Invoke("ShootLaser",5f);
    }

    void ShootLaser()
    {
        shootingBossLaser = true;
        warningLaser.material = warningLaserMat3;
        // Play sound of a laser firing
        Debug.Log("Laser firing");
        Invoke("NotShootingLaserButWillSoon",5f);
    }

    void NotShootingLaserButWillSoon()
    {
        shootingBossLaser = false;
        warningLaser.material = warningLaserMat1;
        Debug.Log("Laser stopped firing");
        Invoke("AboutToShootLaser",5f);
    }

    public void BeginTheLaserPhase()
    {
        Debug.Log("Laser phase begun");
        Invoke("AboutToShootLaser",5f);
    }

    public void BeginTheFinalLaserPhase()
    {
        shootingBossLaser = false;
        warningLaser.material = warningLaserMat1;
        this.CancelInvoke();
        Debug.Log("Final laser phase begun, 30(?) seconds until firing");
        Invoke("FinalLaserFire",30f);
    }

    void FinalLaserFire()
    {
        shootingBossLaser = true;
        warningLaser.material = warningLaserMat3;
        // Play sound of a laser firing
        Debug.Log("Laser firing, final time, instant failure");
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
            if(hit.collider.tag == "Player"){
                 Debug.Log("PLAYER TOUCHED BOSS -> KILL EVENT / GAME OVER");
            }
        }
       } else {
            laserLineR.SetPosition(1, laserOrigin.transform.position);
       }
    }
}
