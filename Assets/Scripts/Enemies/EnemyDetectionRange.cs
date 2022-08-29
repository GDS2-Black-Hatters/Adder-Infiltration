using UnityEngine;

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
