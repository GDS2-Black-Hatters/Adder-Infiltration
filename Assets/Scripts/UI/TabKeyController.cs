using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;

public class TabKeyController : MonoBehaviour
{
    bool isActive = true;
    [SerializeField] private GameObject objectiveList;
    //[SerializeField] private GameObject transparentBG;
    //TODO: Get Icons reference for Tab Key functionality
    //[SerializeField] private GameObject Icons;
    void Awake()
    {
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGameTab, Performed, Hold),
            //new(MainGameTab, Canceled, Release)
        });

        Shader.SetGlobalFloat("_QuestIconMaxAlpha", 1);
    }
    private void Hold(InputAction.CallbackContext callbackContext)
    {
        
        isActive = !isActive;
        objectiveList.SetActive(isActive);
        //transparentBG.SetActive(isActive);
        Shader.SetGlobalFloat("_QuestIconMaxAlpha",isActive? 2:0);
        //Enable Icons when Release Tab
        //Icons.SetActive(isActive);
    } 

    // private void Release(InputAction.CallbackContext callbackContext)
    // {
    //     //Set disabled to both objective list and icons
    //     if(isActive)
    //     {
    //         isActive = false;
    //         objectiveList.SetActive(isActive);
    //         transparentBG.SetActive(isActive);
    //         //Disable Icons when Release Tab
    //         //Icons.SetActive(isActive);
    //     }
    // }
}
