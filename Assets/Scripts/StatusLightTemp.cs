using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusLightTemp : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color LightColor;

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalColor("_StatusEmissionColor", LightColor);
    }
}
