using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(CanvasGroup))]
public sealed class DesktopWindowApplication : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float fadeInSpeed = 0.25f;
    private bool isHovering;

    [Header("Screen Padding"), SerializeField] private float leftPadding;
    [SerializeField] private float rightPadding;
    [SerializeField] private float topPadding;
    [SerializeField] private float bottomPadding = 50;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private readonly Lerper fade = new();
    private Vector2 fadeStartPos;
    private Vector2 fadeEndPos;

    private void Awake()
    {
        rectTransform = (RectTransform)transform;
        rectTransform.pivot = new(0.5f, 0.5f); //Set pivot to emulate window popup.
        rectTransform.localScale = new Vector2(0, 0);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(InputManager.ControlScheme.Hub, GameManager.InputManager.GetAction(InputManager.Controls.Click), ActionInputSubscriber.performed, Prioritise),
        });
    }

    private void Update()
    {
        if (fade.isLerping)
        {
            canvasGroup.alpha = fade.Update(Time.deltaTime);
            rectTransform.anchoredPosition = Vector2.Lerp(fadeStartPos, fadeEndPos, fade.currentValue);
            rectTransform.localScale = Vector2.one * fade.currentValue;
        }
    }

    private Vector2 RandomisePosition()
    {
        RectTransform desktopCanvas = (RectTransform)transform.root;
        float maxX = (desktopCanvas.rect.width - rectTransform.rect.width) * 0.5f;
        float maxY = (desktopCanvas.rect.height - rectTransform.rect.height) * 0.5f;
        float x = Random.Range(leftPadding - maxX, maxX - rightPadding);
        float y = Random.Range(bottomPadding - maxY, maxY - topPadding);
        return new(x, y);
    }

    public void SetStartPos(Vector2 buttonPos)
    {
        fadeStartPos = transform.root.InverseTransformPoint(buttonPos);
    }

    public void ToggleApplication()
    {
        if (fade.isLerping)
        {
            return;
        }

        //Toggles fade in or out.
        float start = canvasGroup.alpha;
        fade.SetValues(start, 1 - start, fadeInSpeed);

        //Toggles the position lerp in or out
        fadeEndPos = start == 0 ? RandomisePosition() : rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void Prioritise(InputAction.CallbackContext eventData)
    {
        if (isHovering)
        {
            Transform grandparent = transform.parent;
            transform.SetParent(transform.root, true);
            transform.SetParent(grandparent, true); //Reattaches at the bottom, prioritising UI on top of others.
        }
    }
}
