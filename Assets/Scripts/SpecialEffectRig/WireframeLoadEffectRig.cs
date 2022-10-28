using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeLoadEffectRig : MonoBehaviour
{
    [SerializeField] private Camera rigCamera;
    [SerializeField] private Transform wireMaskSphereTransform;
    [SerializeField] private Transform plainMaskSphereTransform;
    [SerializeField] private float approachSpeed = 2f;
    [SerializeField] private float expansionRate = 75f;
    [SerializeField] private float plainColorLag = 50f;

    [SerializeField] private bool approaching;
    [SerializeField] private bool isExpanding;

    public UnityEngine.Events.UnityEvent OnApproachComplete;

    public void InitilizeRigPosition(Vector3 position)
    {
        transform.position = position;
        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(90,0,180);
    }

    public void StartRig()
    {
        isExpanding = true;
        approaching = true;
        rigCamera.targetTexture = null;
    }

    private void Update()
    {
        Vector3 towardsPlayerVector = Camera.main.transform.position - transform.position;

        if(approaching)
        {
            transform.Translate(transform.InverseTransformVector(Vector3.ClampMagnitude(towardsPlayerVector,approachSpeed)));
            transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(towardsPlayerVector.normalized), Camera.main.transform.rotation, 1 - towardsPlayerVector.magnitude * 0.025f);
        }

        if(isExpanding)
        {
            wireMaskSphereTransform.localScale += expansionRate * Time.deltaTime * Vector3.one;
            plainMaskSphereTransform.localScale = Mathf.Max(wireMaskSphereTransform.localScale.x - plainColorLag, 1) * Vector3.one;
        }

        if(towardsPlayerVector.sqrMagnitude < 2f)
        {
            OnApproachComplete.Invoke();
        }
    }
}
