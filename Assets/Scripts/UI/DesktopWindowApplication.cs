using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DesktopWindowApplication : MonoBehaviour
{
    [SerializeField] private float fadeInSpeed = 0.25f;
    private RectTransform canvas;
    private CanvasGroup canvasGroup;
    private readonly Lerper fade = new();

    private void Awake()
    {
        canvas = (RectTransform)transform;
        canvas.pivot = new(0, 1); //Set pivot to emulate window popup.
        canvasGroup = GetComponent<CanvasGroup>();
        OnDisable();
    }

    private void OnEnable()
    {
        RandomisePosition();
        fade.SetValues(0, 1, fadeInSpeed);
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (fade.isLerping)
        {
            canvasGroup.alpha = fade.Update(Time.deltaTime);
        }
    }

    private void RandomisePosition()
    {
        RectTransform desktopCanvas = (RectTransform)transform.root;
        float x = Random.Range(0, desktopCanvas.rect.width - canvas.rect.width);
        float y = Random.Range(canvas.rect.height - desktopCanvas.rect.height, 0);
        canvas.anchoredPosition = new(x, y);
    }
}
