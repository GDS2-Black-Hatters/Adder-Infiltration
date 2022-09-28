using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CaughtVariableTracker : MonoBehaviour
{
    protected Image ui;

    protected virtual void Start()
    {
        ui = GetComponent<Image>();
    }
}
