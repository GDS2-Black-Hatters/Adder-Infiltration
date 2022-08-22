using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed = Vector3.one;

    void Update()
    {
        transform.eulerAngles += rotationSpeed * Time.deltaTime;
    }
}
