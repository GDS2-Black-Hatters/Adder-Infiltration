using UnityEngine;

public class MatterShell : MonoBehaviour
{
    [SerializeField] private float matterDistance = 1;
    [SerializeField] private float matterOrbitSpeed = 2;

    [SerializeField] private Matter[] preWeaponMatter;
    [SerializeField] private Matter[] weaponizedMatter;

    private Transform[] matterAnchors;
    private Matter[] activeMatters;

    private void Start()
    {
        matterAnchors = new Transform[Mathf.Max(preWeaponMatter.Length, weaponizedMatter.Length)];

        //Create Anchors
        for (int i = 0; i < matterAnchors.Length; i++)
        {
            RandRotate newAnchorBaseRotator = new GameObject("MatterAnchorBase", typeof(RandRotate)).GetComponent<RandRotate>();
            newAnchorBaseRotator.rotationSpeed = matterOrbitSpeed;
            newAnchorBaseRotator.transform.SetParent(transform);
            newAnchorBaseRotator.transform.localPosition = Vector3.zero;

            matterAnchors[i] = new GameObject("MatterAnchor").transform;
            matterAnchors[i].SetParent(newAnchorBaseRotator.transform);
            matterAnchors[i].localPosition = matterDistance * Random.insideUnitCircle.normalized;
        }

        //Spawn preWeaponMatters
        activeMatters = new Matter[preWeaponMatter.Length];
        for (int i = 0; i < activeMatters.Length; i++)
        {
            activeMatters[i] = Instantiate<Matter>(preWeaponMatter[i],matterAnchors[i]);
            activeMatters[i].InitilizeMatter();
        }
    }

    public void WeaponizeMatter()
    {
        for (int i = 0; i < activeMatters.Length; i++)
        {
            Destroy(activeMatters[i].gameObject);
        }

        activeMatters = new Matter[weaponizedMatter.Length];
        for (int i = 0; i < weaponizedMatter.Length; i++)
        {
            activeMatters[i] = Instantiate<Matter>(weaponizedMatter[i],matterAnchors[i]);
            activeMatters[i].InitilizeMatter();
        }
    }
}
