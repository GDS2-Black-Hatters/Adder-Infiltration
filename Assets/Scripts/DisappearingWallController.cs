using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingWallController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] walls;

    public void setObjectiveComplete(int o)
    {
        switch(o){
            case 1:
                walls[0].SetActive(false);
                break;
            case 2:
                walls[1].SetActive(false);
                walls[2].SetActive(false);
                break;
            case 3:
                walls[3].SetActive(false);
                walls[4].SetActive(false);
                break;
            case 4:
                walls[5].SetActive(false);
                break;
            case 5:
                walls[0].SetActive(true);
                walls[1].SetActive(true);
                walls[2].SetActive(true);
                walls[3].SetActive(true);
                walls[4].SetActive(true);
                walls[5].SetActive(true);
                walls[6].SetActive(false);
                walls[7].SetActive(false);
                walls[8].SetActive(false);
                break;

                
        }
    }
}
