using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;

public class TabKeyController : MonoBehaviour
{
    [SerializeField] private GameObject objectiveList;
    [SerializeField] private GameObject objectiveListCollapse;

    void Awake()
    {
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGameTab, Performed, Hold),
        });
    }

    private void Hold(InputAction.CallbackContext callbackContext)
    {
        ToggleGameObject(objectiveList);
        ToggleGameObject(objectiveListCollapse);
    }
    
    private void ToggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeInHierarchy);
    }
}
