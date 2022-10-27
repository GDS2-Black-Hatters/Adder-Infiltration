using UnityEngine;

public class AwakeSceneScript : MonoBehaviour
{
    [SerializeField] private GameObject text;

    private void Awake()
    {
        if (GameManager.VariableManager.GetVariable<bool>(SaveManager.VariableToSave.tutorialFinish))
        {
            GameManager.LevelManager.ChangeLevel(LevelManager.Level.Hub);
        }
        else
        {
            text.SetActive(true);
        }
    }

    public void GoToTutorial()
    {
        GameManager.LevelManager.ChangeLevel(LevelManager.Level.Tutorial);
    }
}
