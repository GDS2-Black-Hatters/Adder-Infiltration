using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VariableManager;

public class EMP : AbilityBase
{
    private const float minSphereRadius = 1; //Do Not Put 0 otherwise things will not work

    [SerializeField] private Transform chargeSphere;
    [SerializeField] private Transform effectSphere;
    [SerializeField] private float maxRadius = 20;
    [SerializeField] private float chargeRadiusSpeed = 0.5f;
    [SerializeField] private float effectRadialSpeed = 1;

    private readonly List<GameObject> objectsInRange;

    public override AllAbilities AbilityID { get; protected set; } = AllAbilities.EMP;

    protected override void DoAbilityEffect()
    {
        Vector3 size = Vector3.one * minSphereRadius;
        effectSphere.localScale = size;
        chargeSphere.localScale = size;

        chargeSphere.gameObject.SetActive(true);
        StartCoroutine(ChangeChargeSphereRadius(true));

        effectSphere.gameObject.SetActive(true);
        StartCoroutine(EffectTrigger(chargeSphere.localScale.x));
    }

    private IEnumerator ChangeChargeSphereRadius(bool increasing)
    {
        float radiusChangeSpeed = chargeRadiusSpeed * (increasing ? 1 : -1);
        float newRadius = minSphereRadius;
        while (true)
        {
            chargeSphere.position = GameManager.LevelManager.ActiveSceneController.Player.transform.position;
            chargeSphere.localScale = new Vector3(newRadius, newRadius, newRadius);

            newRadius = Mathf.Clamp(chargeSphere.localScale.x + chargeSphere.localScale.x * radiusChangeSpeed * Time.deltaTime, minSphereRadius, maxRadius);
            if (newRadius >= maxRadius || newRadius <= minSphereRadius)
            {
                chargeSphere.gameObject.SetActive(increasing);
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator EffectTrigger(float targetRadius)
    {
        float newRadius = minSphereRadius;
        while (effectSphere.localScale.x < targetRadius)
        {
            effectSphere.position = GameManager.LevelManager.ActiveSceneController.Player.transform.position;
            effectSphere.localScale = new(newRadius, newRadius, newRadius);
            yield return null;
            newRadius = Mathf.Clamp(effectSphere.localScale.x + effectSphere.localScale.x * effectRadialSpeed * Time.deltaTime, minSphereRadius, maxRadius);
        }

        effectSphere.gameObject.SetActive(false);
    }

    public void ObjectEnterStunRange(Collider col)
    {
        //Get component and call stun function... maybe...
    }

    public void NewObjectInRange(Collider col)
    {
        if (objectsInRange.Contains(col.gameObject))
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