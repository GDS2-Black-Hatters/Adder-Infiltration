using UnityEngine;

public class ImageFitToScreen : MonoBehaviour
{
    [SerializeField] private Vector2 sizeRatio;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new(1, 1, distance));
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new(0, 0, distance));

        Vector3 scale = topRight - bottomLeft;
        scale.z = transform.localScale.z;
        transform.localScale = scale;
    }
}
