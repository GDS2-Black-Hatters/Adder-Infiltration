using UnityEngine;

/// <summary>
/// Simple detection range for the enemy.
/// Best to attach this to a child GameObject with a collider.
/// The gameobject should show a mesh to display the detection range.
/// </summary>
public class EnemyDetectionRange : MonoBehaviour
{
    void Update()
    {
        gameObject.SetActive(!GameManager.LevelManager.ActiveSceneController.InCaughtMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.LevelManager.ActiveSceneController.StartCaughtMode();
        }
    }
}
