using UnityEngine;

public class HubParallax : MonoBehaviour
{
    [SerializeField] private float parallaxStrength = 100f;

    private RectTransform rect;
    private Vector2 originalPos;
    private Vector3 originalOffset;

    [SerializeField] private bool useCustomPivotPoint = false;
    [Tooltip("(0, 0) BottomLeft, (1, 1) TopRight"), SerializeField]
    private Vector2 customPivotPoint;


    private Camera cam;

    void Start()
    {
        rect = (RectTransform)transform;
        originalPos = rect.localPosition;

        cam = Camera.main;
        originalOffset = useCustomPivotPoint ? customPivotPoint : cam.ScreenToViewportPoint(transform.position);
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToViewportPoint(Input.mousePosition) - originalOffset;
        rect.localPosition = new()
        {
            x = originalPos.x + (mousePos.x * parallaxStrength),
            y = originalPos.y + (mousePos.y * parallaxStrength),
            z = transform.position.z
        };
    }
}
