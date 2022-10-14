using UnityEngine;

public class ProceduralLevelSceneController : BaseSceneController
{
    [SerializeField] private PCGIsland mainIsland;

    protected override void Start()
    {
        base.Start();

        mainIsland.GenerateIsland();
        SetSpawnPoint();
    }
}
