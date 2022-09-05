using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void ExitLevel()
    {
        GameManager.LevelManager.ChangeLevel(nextScene);
    }
}
