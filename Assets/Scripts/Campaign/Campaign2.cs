using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign2 : MonoBehaviour
{
    public GameObject tutorialGround; 
    public GameObject ShooterSpawner;
    public GameObject BomberSpawner;
    public GameObject BridgeAndPlatformWalls;          

    void Start()
    {
        GameManager.LevelManager.ActiveSceneController.enemyAdmin.OnFullAlert += OnDetected;
    }

    void OnDetected()
    {
        tutorialGround.SetActive(true);
        ShooterSpawner.SetActive(true);
        BomberSpawner.SetActive(true);
        BridgeAndPlatformWalls.SetActive(false);
    }
}
