using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : AbilityBase
{
    private const float minSphereRadius = 1; //Do Not Put 0 otherwise things will not work

    [SerializeField] private GameObject chargeSphere;
    [SerializeField] private GameObject effectSphere;

    [Header("Settings")]
    [SerializeField] private float maxRadius = 20;
    [SerializeField] private float chargeRadiusSpeed = 0.5f;
    [SerializeField] private float effectRadialSpeed = 1;

    private List<GameObject> objectsInRange;

    public override void ActivateAbility()
    {
        effectSphere.transform.localScale = Vector3.one * minSphereRadius;
        effectSphere.SetActive(true);
        chargeSphere.SetActive(false);
        StartCoroutine(EffectTrigger(chargeSphere.transform.localScale.x));
        chargeSphere.transform.localScale = Vector3.one * minSphereRadius;
    }

    public override void EndAbilityPrime()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeChargeSphereRadius(false));
    }

    public override void StartAbilityPrime()
    {
        StopAllCoroutines();
        chargeSphere.SetActive(true);
        StartCoroutine(ChangeChargeSphereRadius(true));
    }

    private IEnumerator ChangeChargeSphereRadius(bool increasing)
    {
        float radiusChangeSpeed = chargeRadiusSpeed * (increasing ? 1 : -1);
        float newRadius = minSphereRadius;
        while (true)
        {
            newRadius = Mathf.Clamp(chargeSphere.transform.localScale.x + chargeSphere.transform.localScale.x * radiusChangeSpeed * Time.deltaTime, minSphereRadius, maxRadius); 
            chargeSphere.transform.localScale = new Vector3(newRadius, newRadius, newRadius);

            if(newRadius >= maxRadius || newRadius <= minSphereRadius)
            {
                chargeSphere.SetActive(increasing);
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator EffectTrigger(float targetRadius)
    {
        float newRadius = minSphereRadius;
        while(effectSphere.transform.localScale.x < targetRadius)
        {
            newRadius = Mathf.Clamp(effectSphere.transform.localScale.x + effectSphere.transform.localScale.x * effectRadialSpeed * Time.deltaTime, minSphereRadius, maxRadius); 
            effectSphere.transform.localScale = new Vector3(newRadius, newRadius, newRadius);

            yield return null;
        }

        effectSphere.SetActive(false);
    }

    public void ObjectEnterStunRange(Collider col)
    {
        //Get component and call stun function... maybe...
    }

    public void NewObjectInRange(Collider col)
    {
        if(objectsInRange.Contains(col.gameObject))
        {
            return;
        }

        objectsInRange.Add(col.gameObject);
    }

    public void ObjectExitRange(Collider col)
    {
        objectsInRange.Remove(col.gameObject);
    }
}
