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

    [SerializeField] private PCGChunkData[] availableChunks;

    private void Start()
    {
        GenerateIsland();
    }

    private void GenerateIsland()
    {
        GameObject ground = Instantiate(groundPrefab, transform.position, Quaternion.identity, transform);
        //Add 1 to generate boarder ground
        ground.transform.localScale = new Vector3((islandSize.x + 1) * cellSizeUnitMultiplier/10, 1, (islandSize.y + 1) * cellSizeUnitMultiplier/10);

        ChunkTransform[] chunkTransforms = BinaryArrayPartition.GetPartitionedChunks(islandSize.x, islandSize.y, () => { return Mathf.FloorToInt(AdvanceRandom.ExponentialRandom(maxChunkSizeLowerBound, expectedChunkSizeVariation)); });

        foreach (ChunkTransform chunkTransform in chunkTransforms)
        {
            //create a copy of chunk data so we don't modify the scriptable object on disk
            PCGChunkData chunkDataCopy = Instantiate(availableChunks[Random.Range(0, availableChunks.Length)]);

            //Initilize and generate chunk
            chunkDataCopy.Initilize(chunkTransform, cellSizeUnitMultiplier);
            GameObject chunkObj = chunkDataCopy.Generate(transform);
            
            //move chunk to new position
            chunkObj.transform.localPosition = GridPosToWorldV3(chunkTransform.ChunkCenter);
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
