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
            Vector3 camNormal = Camera.main.transform.forward;
            Vector3 worldPos = pos.position;
            Vector3 vectorFromCam = worldPos - Camera.main.transform.position;
            targetIcon.gameObject.SetActive(Vector3.Dot(camNormal, vectorFromCam) > 0);
            targetIcon.anchoredPosition = transform.InverseTransformPoint(Camera.main.WorldToScreenPoint(worldPos));
        }
    }
}
