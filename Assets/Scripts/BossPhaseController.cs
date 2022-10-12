using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
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
    
    public void activatePhase(int phaseNumber)
    {
        phase = phaseNumber;
        if(phaseNumber == 2)
        {
            laser.BeginTheLaserPhase();
            foreach(GameObject w in walls)
                //movingWalls = true; //
                w.SetActive(true);
        }
        if(phaseNumber == 3)
        {
            foreach(GameObject f in floors)
                //movingFloors = true; //
                f.SetActive(true);
        }
        if(phaseNumber == 4)
        {
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
        }
    }

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
