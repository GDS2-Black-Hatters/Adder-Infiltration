using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureProgressCircle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] BaseSprites;
    [SerializeField] private SpriteRenderer[] FillSprites;

    public void UpdateProgress(float progress)
    {
        foreach (SpriteRenderer spRend in FillSprites)
        {
            spRend.material.SetFloat("_FillAmount", progress);
        }
    }
}
