using UnityEngine;

public class ObjectiveIcon : MonoBehaviour
{
    private CanvasGroup group;
    [SerializeField] private Transform pos;
    [SerializeField] private RectTransform targetIcon;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.LevelManager.ActiveSceneController.AddIconCanvas(group);
    }

    private void Update()
    {
        if (group.alpha != 0)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, pos.position);
            targetIcon.anchoredPosition = transform.InverseTransformPoint(screenPoint);
        }
    }
}
