using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdmin : MonoBehaviour
{
    [SerializeField] private AINode[] allAiNodes;

    [Header("Alert Status Light Color")]
    [SerializeField, Range(0, 1)] private float alertness = 0;
    [SerializeField, GradientUsage(true, ColorSpace.Linear)] private Gradient worldAlertLightColor;
    [SerializeField, ColorUsage(false, true)] private Color fullAlertColor;
    [SerializeField] private float fullAlertColorLerpTime = 2;
    private float fullAlertColorLerp = 0;


    private void Update()
    {
        //if alertness is basically 1, lerp into full alert color, otherwise evaluate the gradient
        fullAlertColorLerp = Mathf.Clamp( fullAlertColorLerp + Time.deltaTime * (Mathf.Approximately(alertness, 1) ? 1 : -1), 0, fullAlertColorLerpTime);
        Shader.SetGlobalColor("_StatusEmissionColor", Color.Lerp( worldAlertLightColor.Evaluate(alertness), fullAlertColor, fullAlertColorLerp / fullAlertColorLerpTime));
    }

    public void NewAiNodes(AINode[] newAiNodes)
    {
        allAiNodes = newAiNodes;
    }
}
