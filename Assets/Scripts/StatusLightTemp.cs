using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusLightTemp : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color LightColor;

    private void Awake()
    {
        Debug.LogError("Temporary test script in use. REMOVE AT EARLIEST CONVENIENCE!!");
    }

    private void OnValidate()
    {
        Shader.SetGlobalColor("_StatusEmissionColor", LightColor);
    }
}
