using UnityEngine;

/// <summary>
/// Simple detection range for the enemy.
/// Best to attach this to a child GameObject with a collider.
/// The gameobject should show a mesh to display the detection range.
/// </summary>
public class EnemyDetectionRange : MonoBehaviour
{
    private void Update()
    {
        gameObject.SetActive(GameManager.LevelManager.ActiveSceneController.sceneMode == BaseSceneController.SceneState.Stealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.LevelManager.ActiveSceneController.enemyAdmin.IncreaseAlertness(0.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.LevelManager.ActiveSceneController.enemyAdmin.IncreaseAlertness(Time.deltaTime);
        }
    }
}
