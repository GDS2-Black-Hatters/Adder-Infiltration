using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matter : MonoBehaviour
{
    [SerializeField] private MatterObject matterObject;

    public void InitilizeMatter(float matterDistance = 1f)
    {
        matterObject.transform.localPosition = matterDistance * Random.onUnitSphere;
    }
}
