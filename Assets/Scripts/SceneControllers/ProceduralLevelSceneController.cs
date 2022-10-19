using UnityEngine;

public class ProceduralLevelSceneController : BaseSceneController
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PCGIsland mainIsland;

    [SerializeField] private AK.Wwise.Event StartLevelMusicEvent;

    protected override void Start()
    {
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(gameObject.scene);

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
        StartLevelMusicEvent.Post(gameObject);
    }
}
