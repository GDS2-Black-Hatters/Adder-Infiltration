using UnityEngine;

public class ProceduralLevelSceneController : BaseSceneController
{
    [SerializeField] private AK.Wwise.Event StartLevelMusicEvent;

    [SerializeField] private int enemySpawnCount = 100;

    public void OnWorldGenComplete()
    {
        Player.gameObject.SetActive(true);

        SetSpawnPoint();

        enemyAdmin.SpawnEnemies(enemySpawnCount);
/*
        Vector3 position = Player.transform.position + Random.onUnitSphere * 200;
        if(position.y < 0) position.Scale(new(1, -1, 1));
*/
        Vector3 position = Player.transform.position + Vector3.up * 400;
        loadEffectRig.InitilizeRigPosition(position);
    }

    public void CameraApproachComplete()
    {
        Destroy(loadEffectRig.gameObject);
        //StartLevelMusicEvent.Post(gameObject);
    }
}
