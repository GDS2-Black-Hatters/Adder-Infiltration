using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGIsland : MonoBehaviour
{
    private const float cellSizeUnitMultiplier = 25;
    private const float chunkBoarderWidth = 0.5f;

    [SerializeField] private Vector2Int islandSize;
    [SerializeField] private int maxChunkSizeLowerBound = 2;
    [SerializeField] private int expectedChunkSizeVariation = 6;

    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject floorCellPrefab;
    [SerializeField] private GameObject cellFillerPrefab;

    private void Start()
    {
        GenerateIsland();
    }

    private void GenerateIsland()
    {
        GameObject ground = Instantiate(groundPrefab, transform.position, Quaternion.identity, transform);
        //Add 1 to generate boarder ground
        ground.transform.localScale = new Vector3((islandSize.x + 1) * cellSizeUnitMultiplier/10, 1, (islandSize.y + 1) * cellSizeUnitMultiplier/10);

        BinaryArrayPartition.ChunkInfo[] chunks = BinaryArrayPartition.GetPartitionedChunks(islandSize.x, islandSize.y, () => { return Mathf.FloorToInt(AdvanceRandom.ExponentialRandom(maxChunkSizeLowerBound, expectedChunkSizeVariation)); });

        foreach (BinaryArrayPartition.ChunkInfo chunk in chunks)
        {
            //Generate Chunk Floor
            GameObject chunkFloor = Instantiate(floorCellPrefab, GridPosToWorldV3(chunk.ChunkCenter), Quaternion.identity, transform);
            chunkFloor.transform.localScale = Vector3.Scale(chunkFloor.transform.localScale, new Vector3(chunk.ChunkWidth + chunkBoarderWidth, 1, chunk.ChunkHeight + chunkBoarderWidth));

            //Fill Chunk with content
            for(int i = chunk.upperLeft.x; i <= chunk.bottomRight.x; i++)
            {   
                for(int j = chunk.upperLeft.y; j <= chunk.bottomRight.y; j++)
                {
                    GameObject newStructure = Instantiate(cellFillerPrefab, GridPosToWorldV3(i, j), Quaternion.identity);
                    newStructure.transform.SetParent(chunkFloor.transform);
                }
            }
        }
    }

    private Vector3 GridPosToWorldV3(float xCord, float yCord)
    {
        return transform.position + new Vector3( (xCord - (float)(islandSize.x - 1)/2) * cellSizeUnitMultiplier, 0, (yCord - (float)(islandSize.y - 1)/2) * cellSizeUnitMultiplier);
    }

    private Vector3 GridPosToWorldV3(Vector2 gridCord)
    {
        return transform.position + new Vector3((gridCord.x - (float)(islandSize.x - 1)/2) * cellSizeUnitMultiplier, 0, (gridCord.y - (float)(islandSize.y - 1)/2) * cellSizeUnitMultiplier);
    }
}
