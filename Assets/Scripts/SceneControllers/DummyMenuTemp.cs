using UnityEngine;

public class DummyMenuTemp : MonoBehaviour
{
    public void StartGame(string levelToGo)
    {
        GameManager.LevelManager.ChangeLevel(levelToGo);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
