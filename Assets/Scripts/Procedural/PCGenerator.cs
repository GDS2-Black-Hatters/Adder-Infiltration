using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGenerator : MonoBehaviour
{
    [SerializeField] private float cellSizeUnitMultiplier = 25;

    [SerializeField] private PCGeneratableSO[] PossibleGeneratable;

    [SerializeField] private bool generateOnStart;
    [SerializeField] private bool testGenerate;

    private void Start()
    {
        if(generateOnStart)
        {
            PossibleGeneratable[Random.Range(0, PossibleGeneratable.Length)].Generate(transform, cellSizeUnitMultiplier);
        }
    }
    private void OnValidate()
    {
        if(testGenerate)
        {
            testGenerate = false;
            PossibleGeneratable[Random.Range(0, PossibleGeneratable.Length)].Generate(transform, cellSizeUnitMultiplier);
        }
    }

}
