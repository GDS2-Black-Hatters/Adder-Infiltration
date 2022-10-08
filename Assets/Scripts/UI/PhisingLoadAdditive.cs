using UnityEngine;
using UnityEngine.SceneManagement;

public class PhisingLoadAdditive : MonoBehaviour
{
    private bool isActive = false; //Check if active
    private Scene activePhisingScene; //Reference to current Fishing Scene
    public void LoadPhisingScene()
    {
        //Check to avoid multiple Fishing Scenes when spamming
        if(!isActive) 
        {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        activePhisingScene = SceneManager.LoadScene("Fishing", parameters);
        isActive = true;
        }
    }

    public void ClosePhisingScene()
    {   
        if(isActive)
        {
            isActive = false;
            SceneManager.UnloadSceneAsync(activePhisingScene);
        }

    }
}
