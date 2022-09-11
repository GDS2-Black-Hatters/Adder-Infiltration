using UnityEngine;

public class HubParallax : MonoBehaviour
{
    [SerializeField] private float parallaxStrength = 100f;

    private RectTransform rect;
    private Vector2 originalPos;
    private Camera cam;
    
    void Start()
    {
        rect = (RectTransform)transform;
        originalPos = rect.localPosition;
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f);
        rect.localPosition = new()
        {
            x = originalPos.x + (mousePos.x * parallaxStrength),
            y = originalPos.y + (mousePos.y * parallaxStrength),
            z = transform.position.z
        };
    }
}
