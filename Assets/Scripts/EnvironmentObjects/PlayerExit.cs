using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField] private LevelManager.Level nextScene;
    [SerializeField] private bool incrementMoney = false; //TODO: Probably remove this later

    public void ExitLevel()
    {
        if (GameManager.LevelManager.ActiveSceneController.canFinish)
        {
            if (incrementMoney)
            {
                GameManager.VariableManager.RandomIncrement();
                Debug.LogError("Remove this later.");
            }

            GameManager.LevelManager.ChangeLevel(nextScene);
        }
    }
}
