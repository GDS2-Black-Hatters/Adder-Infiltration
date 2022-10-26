using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLoadAdditive : MonoBehaviour
{
    private bool isActive = false; //Check if active
    private Scene activeScene; //Reference to current Fishing Scene
    [SerializeField] private SceneAsset scene;

    public void ToggleMinigameScene()
    {
        //Check to avoid multiple Minigame Scenes when spamming
        if (!isActive)
        {
            isActive = true;
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            activeScene = SceneManager.LoadScene(scene.name, parameters);
        }
        else
        {
            isActive = false;
            SceneManager.UnloadSceneAsync(activeScene);
        }
    }
}
