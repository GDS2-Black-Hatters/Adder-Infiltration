using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerator : MonoBehaviour
{
    [SerializeField] private PCGBuildingData buildingData;

    void Start()
    {
        buildingData.Generate(transform);
        Destroy(this);
    }
}
