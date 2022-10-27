using UnityEngine;

public class ObjectiveLock : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    void Update()
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(GameManager.LevelManager.ActiveSceneController.canFinish);
        }
    }
}
