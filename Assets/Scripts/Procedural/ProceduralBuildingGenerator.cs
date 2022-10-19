using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerator : PCGenerator
{
    [SerializeField] private PCGBuildingData buildingData;

    protected override IEnumerator Generate()
    {
        buildingData.Generate(transform, new GameObject(), this, GenerationIncomplete);
        yield break;
    }
}
