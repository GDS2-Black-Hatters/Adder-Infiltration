using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGBuildingGenerator : MonoBehaviour
{
    [SerializeField] private PCGBuildingData BuildingGenData;

    [SerializeField] private bool generateOnStart;
    [SerializeField] private bool testGenerate;

    private void Start()
    {
        if(generateOnStart)
        {
            BuildingGenData.Generate(transform);
        }
    }
    private void OnValidate()
    {
        if(testGenerate)
        {
            testGenerate = false;
            BuildingGenData.Generate(transform);
        }
    }
}
