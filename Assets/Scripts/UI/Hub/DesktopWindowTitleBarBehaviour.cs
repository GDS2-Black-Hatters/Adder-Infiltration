using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;

public class DesktopWindowTitleBarBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering;
    private bool isPressed = false;
    private Vector2 windowDistance;

    private void Start()
    {
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(HubClick, Performed, OnHoverClick),
            new(HubClick, Canceled, OnHoverUnclick),
            new(HubMove, Performed, OnFocusMove),
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
        if (isPressed = isHovering)
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
