using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralIslandGenerator : PCGenerator
{
    [SerializeField] private PCGIslandData islandData;

    [SerializeField] private Vector2Int islandSize;
    [SerializeField] private int maxChunkSizeLowerBound = 9;
    [SerializeField] private int expectedChunkSizeVariation = 10;

    protected override IEnumerator Generate()
    {
        GenerationIncomplete();
        PCGIslandData islandDataCopy = Instantiate(islandData);
        islandDataCopy.Initilize(islandSize, maxChunkSizeLowerBound, expectedChunkSizeVariation);

        yield return StartCoroutine(islandDataCopy.Generate(transform, new GameObject(), this, GenerationIncomplete));
    }
}
