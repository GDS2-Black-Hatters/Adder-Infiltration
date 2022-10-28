using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLoadAdditive : MonoBehaviour
{
    private bool isActive = false; //Check if active
    private Scene activeScene; //Reference to current Fishing Scene
    private string sceneName;

#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset scene;
    private void OnValidate()
    {
        sceneName = scene.name;
    }
#endif

    public void ToggleMinigameScene()
    {
        //Check to avoid multiple Minigame Scenes when spamming
        if (!isActive)
        {
            isActive = true;
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            activeScene = SceneManager.LoadScene(sceneName, parameters);
        }
        else
        {
            isActive = false;
            SceneManager.UnloadSceneAsync(activeScene);
        }
    }
}
