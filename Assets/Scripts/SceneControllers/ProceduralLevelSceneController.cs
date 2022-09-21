using UnityEngine;

public class ProceduralLevelSceneController : BaseSceneController
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PCGIsland mainIsland;

    protected override void Start()
    {
        base.Start();

        mainIsland.GenerateIsland();
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        SetSpawnPoint();
    }
}
