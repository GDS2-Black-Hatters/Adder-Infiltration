using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundRend;
    [SerializeField] private SpriteRenderer progressRingRend;


    public void UpdateProgress(float progress)
    {
        progressRingRend.material.SetFloat("_FillAmount", progress);
    }
}
