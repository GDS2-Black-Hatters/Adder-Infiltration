using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralIslandGenerator : MonoBehaviour
{
    [SerializeField] private PCGIslandData islandData;

    [SerializeField] private Vector2Int islandSize;
    [SerializeField] private int maxChunkSizeLowerBound = 9;
    [SerializeField] private int expectedChunkSizeVariation = 10;


    void Start()
    {
        PCGIslandData islandDataCopy = Instantiate(islandData);
        islandDataCopy.Initilize(islandSize, maxChunkSizeLowerBound, expectedChunkSizeVariation);

        islandDataCopy.Generate(transform);

        Destroy(this);
    }
}
