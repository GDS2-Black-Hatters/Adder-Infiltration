using UnityEngine;
using UnityEngine.UI;

public class CanvasAutoAdjust : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.pixelPerfect = true;
        canvas.worldCamera = Camera.main;

        CanvasScaler scaler = GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

    private void Update()
    {
        canvas.worldCamera = canvas.worldCamera != null ? canvas.worldCamera : Camera.main;
    }
}