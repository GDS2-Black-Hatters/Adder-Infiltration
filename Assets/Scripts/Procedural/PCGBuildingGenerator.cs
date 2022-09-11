using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGBuildingGenerator : MonoBehaviour
{
    [SerializeField] private PCGBuildingData[] PossibleBuildingGenData;

    [SerializeField] private bool generateOnStart;
    [SerializeField] private bool testGenerate;

    private void Start()
    {
        if(generateOnStart)
        {
            PossibleBuildingGenData[Random.Range(0, PossibleBuildingGenData.Length)].Generate(transform);
        }
    }
    private void OnValidate()
    {
        if(testGenerate)
        {
            testGenerate = false;
            PossibleBuildingGenData[Random.Range(0, PossibleBuildingGenData.Length)].Generate(transform);
        }
    }
}
