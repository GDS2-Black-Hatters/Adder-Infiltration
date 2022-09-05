using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void ExitLevel()
    {
        if (GameManager.LevelManager.ActiveSceneController.canFinish)
        {
            GameManager.LevelManager.ChangeLevel(nextScene);
        }
    }
}
