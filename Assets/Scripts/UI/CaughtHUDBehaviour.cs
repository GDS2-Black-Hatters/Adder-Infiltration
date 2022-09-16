using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CaughtHUDBehaviour : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float fadeInSpeed = 1;
    private Lerper lerp;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        lerp = new();
    }

    private void Update()
    {
        if (lerp.isLerping)
        {
            lerp.Update(Time.deltaTime);
            canvasGroup.alpha = lerp.currentValue;
        }
    }

    public void FadeIn()
    {
        lerp.SetValues(0, 1, fadeInSpeed);
    }

    public void HideHUD()
    {
        canvasGroup.alpha = 0;
    }
}
