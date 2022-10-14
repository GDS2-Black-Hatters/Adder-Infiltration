using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossPhaseController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] walls, floors, objectiveObjects;
    [SerializeField]
    private GameObject lastWall;
    [SerializeField]
    private int phase = 0;

    public FinalBossLaser laser;

    [SerializeField]
    private FinalBossObjective[] objectives;

    private bool movingWalls = false, movingFloors = false;

    public TextMeshProUGUI objectiveList;

    private void Start()
    {
        exitZoneLaser.enabled = false;
        objectiveList.text = "Activate the four corners to enable the antivirus boss.";
        foreach(GameObject f in floors)
            //f.transform.position = new Vector3(f.transform.position.x, hiddenFloorPosition,f.transform.position.z);
                f.SetActive(false);
        foreach(GameObject w in walls)
            //w.transform.position = new Vector3(w.transform.position.x, hiddenWallPosition,w.transform.position.z);
                w.SetActive(false);
    }

    private void Update()
    {
        int i = 0;
        foreach(FinalBossObjective o in objectives)
        {
            if(o.complete)
                i++;
        }
        if(i == 4)
        {
            activatePhase(phase+1);
            foreach(FinalBossObjective o in objectives)
            {
                o.ResetProgress();
            }
        }

        moveWallsAndFloors();
    }

// Phase 2 = when all objectives done once
// 3 = twice
// 4 = the 'escape sequence', timer or something forcing the player to get out (last wall hidden, all hazards disabled)
    
    public FinalBossLaser bossLaser;

    public void activatePhase(int phaseNumber)
    {

        bossLaser.ResetPosition();

        phase = phaseNumber;
        if(phaseNumber == 1)
        {
            objectiveList.text = "The antivirus is active!\nActivate the suppression lasers again to defeat it.\nDon't let it touch you, or you will be deleted.";
            bossLaser.NextPhaseSpeed(1);
        }
        if(phaseNumber == 2)
        {
            objectiveList.text = "[ALERT] Hostile Program Detected. Initiating Laser Countermeasure.\nLaser Powering Up. 5 Second Warmup Time.";
            laser.BeginTheLaserPhase();
            foreach(GameObject w in walls)
                //movingWalls = true; //
                w.SetActive(true);
        }
        if(phaseNumber == 3)
        {
            objectiveList.text = "[ALERT] Hostile Program Evading Attacks. Enabling Underclocking Devices.\n- Don't touch the yellow floors or you will be slowed.";
            bossLaser.NextPhaseSpeed(2);
            foreach(GameObject f in floors)
                //movingFloors = true; //
                f.SetActive(true);
        }
        if(phaseNumber == 4)
        {
             objectiveList.text = "[WARNING] Total Data Erasure In 30 Seconds.\n - The antivirus is using one last attack, get out of there!";
            laser.BeginTheFinalLaserPhase();
            lastWall.SetActive(false);
            foreach(GameObject f in floors)
                //movingFloors = true; //
                f.SetActive(false);
            foreach(GameObject w in walls)
                //movingWalls = true; //
                w.SetActive(false);
            foreach(GameObject o in objectiveObjects)
                o.SetActive(false);

            exitZoneLaser.enabled = true;
            exitZoneLaser.SetPosition(0,exitZoneLaserStart.transform.position);
            exitZoneLaser.SetPosition(1,exitZoneLaserDestination.transform.position);
        }
    }

    public LineRenderer exitZoneLaser;
    public Transform exitZoneLaserStart, exitZoneLaserDestination;

    void moveWallsAndFloors()
    {
        if(phase != 4)
        {
            if(movingFloors)
            {
                foreach(GameObject f in floors)
                    while(f.transform.position.y < finalFloorPosition)
                            f.transform.Translate(0,1*Time.deltaTime,0);
            }
            if(movingWalls)
            {
                foreach(GameObject w in walls)
                    while(w.transform.position.y < finalWallPosition)
                            w.transform.Translate(0,1*Time.deltaTime,0);
                
            }
        }
        else
        {
            if(movingFloors)
            {
                foreach(GameObject f in floors)
                    while(f.transform.position.y > hiddenFloorPosition)
                            f.transform.Translate(0,-1*Time.deltaTime,0);
            }
            if(movingWalls)
            {
                foreach(GameObject w in walls)
                    while(w.transform.position.y > hiddenWallPosition)
                            w.transform.Translate(0,-1*Time.deltaTime,0);
                
            }
        }
    }

    int finalWallPosition = 39, hiddenWallPosition = -111,
        finalFloorPosition = -6, hiddenFloorPosition = -17;


}
