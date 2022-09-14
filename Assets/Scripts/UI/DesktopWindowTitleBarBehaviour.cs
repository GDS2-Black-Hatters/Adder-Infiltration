using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallBackContext;
using static InputManager.Controls;
using static InputManager.ControlScheme;

public class DesktopWindowTitleBarBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InputManager input;
    private bool isHovering;
    private bool isPressed = false;

    private void Start()
    {
        input = GameManager.InputManager;

        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(Hub, input.GetAction(Click), Performed, OnHoverClick),
            new(Hub, input.GetAction(Click), Canceled, OnHoverUnclick),
            new(Hub, input.GetAction(Move), Started, OnFocusStartMove),
            new(Hub, input.GetAction(Move), Performed, OnFocusMove),
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

    private void OnFocusStartMove(InputAction.CallbackContext moveDelta)
    {

    }

    private void OnFocusMove(InputAction.CallbackContext moveDelta)
    {
        if (isPressed)
        {
            transform.parent.Translate(moveDelta.ReadValue<Vector2>());
        }
    }
}
