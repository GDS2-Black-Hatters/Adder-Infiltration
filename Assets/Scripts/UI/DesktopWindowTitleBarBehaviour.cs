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
    private Vector2 windowDistance;

    private void Start()
    {
        input = GameManager.InputManager;

        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(Hub, input.GetAction(Click), Performed, OnHoverClick),
            new(Hub, input.GetAction(Click), Canceled, OnHoverUnclick),
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
        if (isPressed)
        {
            RectTransform rectTransform = (RectTransform)transform.root;
            Vector2 half = new(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f);
            Vector2 mouseStartPosition = rectTransform.rect.size * Camera.main.ScreenToViewportPoint(Input.mousePosition) - half;
            windowDistance = mouseStartPosition - (Vector2)transform.root.InverseTransformPoint(transform.parent.position);
        }
    }

    private void OnHoverUnclick(InputAction.CallbackContext moveDelta)
    {
        isPressed = false;
    }

    private void OnFocusMove(InputAction.CallbackContext moveDelta)
    {
        if (isPressed)
        {
            RectTransform rectTransform = (RectTransform)transform.root;
            Vector2 half = new(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f);
            Vector2 mousePosition = rectTransform.rect.size * Camera.main.ScreenToViewportPoint(Input.mousePosition) - half;
            ((RectTransform)transform.parent).anchoredPosition = mousePosition - windowDistance;
        }
    }
}
