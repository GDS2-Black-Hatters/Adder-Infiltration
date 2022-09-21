using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeLoadEffectRig : MonoBehaviour
{
    [SerializeField] private Transform wireMaskSphereTransform;
    [SerializeField] private Transform plainMaskSphereTransform;
    [SerializeField] private float expansionRate = 75f;
    [SerializeField] private float plainColorLag = 50f;

    [SerializeField] private bool isExpanding;

    private void Update()
    {
        if(isExpanding)
        {
            wireMaskSphereTransform.localScale += expansionRate * Time.deltaTime * Vector3.one;
            plainMaskSphereTransform.localScale = Mathf.Max(wireMaskSphereTransform.localScale.x - plainColorLag, 1) * Vector3.one;
        }
    }
}
