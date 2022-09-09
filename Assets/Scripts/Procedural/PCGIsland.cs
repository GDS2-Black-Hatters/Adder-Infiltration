using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGIsland : MonoBehaviour
{
    private const float cellSizeUnitMultiplier = 50;

    [SerializeField] private Vector2Int islandSize;
    [SerializeField] private int maxChunkSizeLowerBound = 2;
    [SerializeField] private int expectedChunkSizeVariation = 6;

    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject floorCellPrefab;

    private int[,] cellArray;

    private void Start()
    {
        GenerateIsland();
    }

    private void GenerateIsland()
    {
        GameObject ground = Instantiate(groundPrefab, transform.position, Quaternion.identity, transform);
        //Add 1 to generate boarder ground
        ground.transform.localScale = new Vector3((islandSize.x + 1) * cellSizeUnitMultiplier/10, 1, (islandSize.y + 1) * cellSizeUnitMultiplier/10);

        cellArray = new int[islandSize.x, islandSize.y];

        BinaryArrayPartition.ChunkInfo[] chunks = BinaryArrayPartition.PartitionArray<int>(ref cellArray, 1, (int i) => { return i==0; }, () => { return Mathf.FloorToInt(AdvanceRandom.ExponentialRandom(maxChunkSizeLowerBound, expectedChunkSizeVariation)); });

        for(int i = 0; i < islandSize.x; i++)
        {   
            for(int j = 0; j < islandSize.y; j++)
            {
                if(cellArray[i,j] == 1)
                {
                    Instantiate(floorCellPrefab, new Vector3((i - (float)(islandSize.x - 1)/2) * cellSizeUnitMultiplier, 0, (j - (float)(islandSize.y - 1)/2) * cellSizeUnitMultiplier), Quaternion.identity, transform);
                }
            }
        }

        foreach (BinaryArrayPartition.ChunkInfo chunk in chunks)
        {
            //Debug.Log("Chunk: UL:" + chunk.upperLeft + ",BR:" + chunk.bottomRight);
        }
    }
}
