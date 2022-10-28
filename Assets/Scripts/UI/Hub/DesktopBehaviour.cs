using UnityEngine;
using static InputManager;
using static ActionInputSubscriber.CallbackContext;
using UnityEngine.InputSystem;

public class DesktopBehaviour : MonoBehaviour
{
    [SerializeField] protected AK.Wwise.Event desktopClickSFXEvent;

    private void Awake()
    {
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(HubClick, Performed, (InputAction.CallbackContext callback) => { desktopClickSFXEvent.Post(gameObject); }),
        });
    }
}
