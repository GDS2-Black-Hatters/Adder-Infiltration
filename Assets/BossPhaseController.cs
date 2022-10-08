using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] walls, floors;
    [SerializeField]
    private GameObject lastWall;

// Phase 2 = when all objectives done once
// 3 = twice
// 4 = the 'escape sequence', timer or something forcing the player to get out (last wall hidden, all hazards disabled)
    
    public void activatePhase(int phaseNumber)
    {
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
        }
    }

}
