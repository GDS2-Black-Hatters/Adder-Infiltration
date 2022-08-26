using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEnvironmentObject : MonoBehaviour
{
    public void AlertPlayerDetection()
    {
        LevelSceneController lsc = (LevelSceneController)GameManager.LevelManager.ActiveSceneController;
        if(lsc == null)
        {
            Debug.LogWarning("Cannot find LevelSceneController, will do nothing on player detection.");
            return;
        }
        lsc.PlayerDetected();        
    }
}
