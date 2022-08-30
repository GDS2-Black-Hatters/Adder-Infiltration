using UnityEngine;

public class DetectorEnvironmentObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.LevelManager.ActiveSceneController.StartCaughtMode();
        }
    }
}
