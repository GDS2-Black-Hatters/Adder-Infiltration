using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureProgressCircle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] BaseSprites;
    [SerializeField] private SpriteRenderer[] FillSprites;

    private MaterialPropertyBlock[] mpbs;

    public void Start()
    {
        mpbs= new MaterialPropertyBlock[FillSprites.Length];
        for(int i = 0; i < FillSprites.Length; i++)
        {
            mpbs[i] = new();
            FillSprites[i].GetPropertyBlock(mpbs[i]);
        }
    }

    public void UpdateProgress(float progress)
    {
        for(int i = 0; i < FillSprites.Length; i++)
        {
            mpbs[i].SetFloat("_FillAmount", progress);
            FillSprites[i].SetPropertyBlock(mpbs[i]);
        }
    }
}
