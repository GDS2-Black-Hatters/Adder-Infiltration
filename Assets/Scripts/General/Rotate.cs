using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed = Vector3.one;

    void Update()
    {
        transform.Rotate(rotationSpeed * 360 * Time.deltaTime);
    }
}
