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
    [SerializeField] private GameObject cellFillerPrefab;

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
                    Instantiate(floorCellPrefab, GridPosToWorldV3(i, j), Quaternion.identity, transform);
                }
            }
        }

        foreach (BinaryArrayPartition.ChunkInfo chunk in chunks)
        {
            for(int i = chunk.upperLeft.x; i <= chunk.bottomRight.x; i++)
            {   
                for(int j = chunk.upperLeft.y; j <= chunk.bottomRight.y; j++)
                {
                    Instantiate(cellFillerPrefab, GridPosToWorldV3(i, j), Quaternion.identity, transform);
                }
            }
        }
    }

    private Vector3 GridPosToWorldV3(int xCord, int yCord)
    {
        return new Vector3((xCord - (float)(islandSize.x - 1)/2) * cellSizeUnitMultiplier, 0, (yCord - (float)(islandSize.y - 1)/2) * cellSizeUnitMultiplier);
    }

    private Vector3 GridPosToWorldV3(Vector2Int gridCord)
    {
        return new Vector3((gridCord.x - (float)(islandSize.x - 1)/2) * cellSizeUnitMultiplier, 0, (gridCord.y - (float)(islandSize.y - 1)/2) * cellSizeUnitMultiplier);
    }
}
