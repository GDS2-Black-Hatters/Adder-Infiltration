using UnityEngine;

public class CamFollow : MonoBehaviour
{
    void Update()
    {
        Transform cam = Camera.main.transform;
        cam.position = transform.position;
        cam.eulerAngles = transform.eulerAngles;
    }
}
