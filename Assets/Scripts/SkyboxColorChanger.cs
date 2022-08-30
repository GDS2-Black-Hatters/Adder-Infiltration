using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorChanger : MonoBehaviour
{
    [SerializeField] Color startColor = Color.black;
    [SerializeField] Color endColor = Color.white;

    [SerializeField] float lerpSpeed = 1f;
    private float lerpProgress = 0f;

    public void StartColorLerp()
    {
        StartCoroutine(LerpColor());
    }

    private IEnumerator LerpColor()
    {
        //Create and assign a copy so we don't change the asset original values
        Material skyboxMatCopy = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyboxMatCopy;
        while(lerpProgress <= 1)
        {
            lerpProgress += Time.deltaTime * lerpSpeed;
            RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(startColor, endColor, lerpProgress));
            yield return null;
        }
    }
}
