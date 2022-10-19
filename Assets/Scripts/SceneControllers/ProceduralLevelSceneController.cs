using UnityEngine;

public class ProceduralLevelSceneController : BaseSceneController
{
    [SerializeField] private GameObject playerPrefab;


    [SerializeField] private AK.Wwise.Event StartLevelMusicEvent;

    [SerializeField] private int enemySpawnCount = 100;

    public void OnWorldGenComplete()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        SetSpawnPoint();

        enemyAdmin.SpawnEnemies(enemySpawnCount);

        Vector3 position = GameManager.LevelManager.player.transform.position + Random.onUnitSphere * 200;
        if(position.y < 0) position.Scale(new(1, -1, 1));
        loadEffectRig.InitilizeRigPosition(position);
    }

    public void CameraApproachComplete()
    {
        Destroy(loadEffectRig.gameObject);
        StartLevelMusicEvent.Post(gameObject);
    }
}
