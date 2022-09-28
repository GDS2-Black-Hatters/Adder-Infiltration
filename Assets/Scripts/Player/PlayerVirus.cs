using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVirus : MonoBehaviour
{
    [field: SerializeField] public PlayerVirusController virusController { get; private set; }
    [field: SerializeField] public GameObject playerCoreObj;

    private void Awake()
    {
        GameManager.LevelManager.SetPlayer(this);
    }
}
