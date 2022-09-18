using UnityEngine;

public class ProceduralLevelSceneController : BaseSceneController
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PCGIsland mainIsland;

    protected void Start()
    {
        mainIsland.GenerateIsland();
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        SetSpawnPoint();
    }
}
