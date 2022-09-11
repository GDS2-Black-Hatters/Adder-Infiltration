using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField] private LevelManager.Level nextScene;

    public void ExitLevel()
    {
        if (GameManager.LevelManager.ActiveSceneController.canFinish)
        {
            GameManager.LevelManager.ChangeLevel(nextScene);
        }
    }
}
