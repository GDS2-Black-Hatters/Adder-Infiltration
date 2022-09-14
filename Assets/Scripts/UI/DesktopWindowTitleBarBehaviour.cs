using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DesktopWindowTitleBarBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InputManager input;
    private bool isHovering;
    private bool isPressed = false;

    private void Start()
    {
        input = GameManager.InputManager;
        
        InputManager.ControlScheme hub = InputManager.ControlScheme.Hub;

        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(hub, input.GetAction(InputManager.Controls.Click), ActionInputSubscriber.performed, OnHoverClick),
            new(hub, input.GetAction(InputManager.Controls.Click), ActionInputSubscriber.canceled, OnHoverUnclick),
            new(hub, input.GetAction(InputManager.Controls.Move), ActionInputSubscriber.performed, OnFocusMove),
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void OnHoverClick(InputAction.CallbackContext moveDelta)
    {
        isPressed = isHovering;
    }

    private void OnHoverUnclick(InputAction.CallbackContext moveDelta)
    {
        isPressed = false;
    }

    private void OnFocusMove(InputAction.CallbackContext moveDelta)
    {
        if (isPressed)
        {
            //Move gameobject here.
        }
    }
}
