using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField] private LevelManager.Level nextScene;
    [SerializeField] private bool incrementMoney = false;

    public void ExitLevel()
    {
        if (GameManager.LevelManager.ActiveSceneController.canFinish)
        {
            if (incrementMoney)
            {
                GameManager.VariableManager.RandomIncrement();
            }

            GameManager.LevelManager.ChangeLevel(nextScene);
        }
    }
}
