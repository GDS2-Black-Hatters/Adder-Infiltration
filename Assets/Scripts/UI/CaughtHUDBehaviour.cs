using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CaughtHUDBehaviour : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float fadeInSpeed = 1;
    private bool isOn = false;
    private Lerper lerp;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        lerp = new();
    }

    private void Update()
    {
        BaseSceneController controller = GameManager.LevelManager.ActiveSceneController;
        if (isOn)
        {
            isOn = controller;
            return;
        }

        if (lerp.isLerping)
        {
            lerp.Update(Time.deltaTime);
            canvasGroup.alpha = lerp.currentValue;
            isOn = !lerp.isLerping;
            return;
        }

        if (controller && controller.sceneMode != BaseSceneController.SceneState.Stealth)
        {
            lerp.SetValues(0, 1, fadeInSpeed);
        }
    }
}
