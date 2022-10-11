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
    private int phase = 1;

    [SerializeField]
    private FinalBossObjective[] objectives;

    private void Start()
    {
        foreach(GameObject f in floors)
                f.SetActive(false);
        foreach(GameObject w in walls)
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
    }

// Phase 2 = when all objectives done once
// 3 = twice
// 4 = the 'escape sequence', timer or something forcing the player to get out (last wall hidden, all hazards disabled)
    
    public void activatePhase(int phaseNumber)
    {
        phase = phaseNumber;
        if(phaseNumber == 2)
        {
            foreach(GameObject w in walls)
                w.SetActive(true);
        }
        if(phaseNumber == 3)
        {
            foreach(GameObject f in floors)
                f.SetActive(true);
        }
        if(phaseNumber == 4)
        {
            lastWall.SetActive(false);
            foreach(GameObject f in floors)
                f.SetActive(false);
            foreach(GameObject w in walls)
                w.SetActive(false);
            foreach(GameObject o in objectiveObjects)
                o.SetActive(false);
        }
    }

}
