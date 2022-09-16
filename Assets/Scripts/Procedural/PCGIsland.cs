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

    [SerializeField] private PCGChunkData[] requiredChunks;
    [SerializeField] private PCGChunkData[] availableChunks;

    private class IslandChunk
    {
        public ChunkTransform chunkTransform;
        public PCGChunkData chunkData;
    }

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

        //VerifyChunkAvailability(requiredChunks, chunkTransforms);

        foreach (ChunkTransform chunkTransform in chunkTransforms)
        {
            //create a copy of chunk data so we don't modify the scriptable object on disk
            PCGChunkData chunkDataCopy = Instantiate(availableChunks[Random.Range(0, availableChunks.Length)]);

            //Initilize and generate chunk
            chunkDataCopy.Initilize(chunkTransform);
            GameObject chunkObj = chunkDataCopy.Generate(transform, cellSizeUnitMultiplier);
            
            //move chunk to new position
            chunkObj.transform.localPosition = GridPosToWorldV3(chunkTransform.ChunkCenter);
        }
    }

    private bool VerifyChunkAvailability(PCGChunkData[] requiredChunks, ChunkTransform[] availableChunks)
    {
        //Fail if there aren't enough chunks
        if(availableChunks.Length < requiredChunks.Length)
            return false;

        //Verify there are enough chunk of required size
        List<int> availableChunkSizeCount = new List<int>();
        foreach(ChunkTransform ct in availableChunks)
        {
            if(availableChunkSizeCount.Count <= ct.ChunkCellCount)
            {
                availableChunkSizeCount.AddRange(new int[ct.ChunkCellCount - availableChunkSizeCount.Count + 1]);
            }
            availableChunkSizeCount[ct.ChunkCellCount] += 1;
        }
        List<int> requiredChunkSizeCount = new List<int>();
        foreach(PCGChunkData rc in requiredChunks)
        {
            if(requiredChunkSizeCount.Count <= rc.minCellCountRequirement)
            {
                requiredChunkSizeCount.AddRange(new int[rc.minCellCountRequirement - requiredChunkSizeCount.Count + 1]);
            }
            requiredChunkSizeCount[rc.minCellCountRequirement] += 1;
        }
        for (int i = requiredChunkSizeCount.Capacity; i > 0; i--)
        {
            for(int j = availableChunkSizeCount.Capacity; j > 0; j--)
            {

            }
        }


        return true;
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
