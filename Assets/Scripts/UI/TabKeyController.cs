using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;

public class TabKeyController : MonoBehaviour
{
    bool isActive = true;
    [SerializeField] private GameObject objectiveList;

    void Awake()
    {
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGameTab, Performed, Hold),
        });
    }

    private void Hold(InputAction.CallbackContext callbackContext)
    {
        
        isActive = !isActive;
        objectiveList.SetActive(isActive);
    }
}
