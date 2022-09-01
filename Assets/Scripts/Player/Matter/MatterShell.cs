using UnityEngine;

public class MatterShell : MonoBehaviour
{
    private const int TargetingRange = 10;
    private const int MaxEnemyInRange = 100;

    [SerializeField] private float matterDistance = 1;
    [SerializeField] private float matterOrbitSpeed = 2;

    [SerializeField] private Matter[] preWeaponMatter;
    [SerializeField] private WeaponMatter[] weaponizedMatter;

    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] LayerMask targetOcclutionMask;
    private Collider[] enemiesInRange = new Collider[MaxEnemyInRange];
    private int currentEnemiesInRange = 0;

    private Transform[] matterAnchors;
    private Matter[] activeMatters;

    //currentTarget currently would keep track of the nearest 
    private Transform _currentTarget; //Do not modify this directly, use currentTarget property.
    public Transform currentTarget{
        get{ return _currentTarget; }
        private set{
            if(_currentTarget == value)
            {
                return;
            }
            _currentTarget = value;
            OnTargetChange?.Invoke(_currentTarget);
        }
    }
    public System.Action<Transform> OnTargetChange;

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
            activeMatters[i].InitilizeMatter(this, matterAnchors[i].parent);
        }
    }

    private void Update()
    {
        UpdateTargets();
    }

    private void UpdateTargets()
    {
        currentEnemiesInRange = Physics.OverlapSphereNonAlloc(transform.position, TargetingRange, enemiesInRange, targetLayerMask);

        Transform tempBestTarget = null;

        //loop through every result and 
        for (int i = 0; i < currentEnemiesInRange; i++)
        {
            RaycastHit hitResult;
            Physics.Raycast(transform.position, enemiesInRange[i].transform.position - transform.position, out hitResult, TargetingRange, targetOcclutionMask);
            if(hitResult.collider == enemiesInRange[i])
            {
                tempBestTarget = enemiesInRange[i].transform;
            }
        }

        //Assign temp result to current target
        currentTarget = tempBestTarget;
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
            activeMatters[i].InitilizeMatter(this, matterAnchors[i].parent);
        }
    }
}
