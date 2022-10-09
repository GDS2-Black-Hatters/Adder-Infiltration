using UnityEngine;
using UnityEngine.SceneManagement;

public class PhishingLoadAdditive : MonoBehaviour
{
    private bool isActive = false; //Check if active
    private Scene activePhisingScene; //Reference to current Fishing Scene

    public void TogglePhishingScene()
    {
        //Check to avoid multiple Fishing Scenes when spamming
        if (!isActive)
        {
            isActive = true;
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            activePhisingScene = SceneManager.LoadScene("Fishing", parameters);
        }
        else
        {
            isActive = false;
            SceneManager.UnloadSceneAsync(activePhisingScene);
        }
    }
}
