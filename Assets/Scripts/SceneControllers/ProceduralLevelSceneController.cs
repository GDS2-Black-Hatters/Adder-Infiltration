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

        //TODO: This is temporary
        Invoke("TempDestroyRig", 10);
    }

    [System.Obsolete]
    private void TempDestroyRig()
    {
        Destroy(loadEffectRig.gameObject);
    }
}
