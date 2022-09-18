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

    [SerializeField] private GameObject roadwayGroundPrefab;

    [SerializeField] private PCGChunkData[] requiredChunks;
    [SerializeField] private PCGChunkData[] availableChunks;

    [SerializeField] private bool generateOnStart = false;

    private class IslandChunk
    {
        public ChunkTransform chunkTransform;
        public PCGChunkData chunkData;
    }

    private void Start()
    {
        if(generateOnStart)
            GenerateIsland();
    }

    public void GenerateIsland()
    {
        GameObject ground = Instantiate(roadwayGroundPrefab, transform.position, Quaternion.identity, transform);
        //Add 1 to generate boarder ground
        float calculatedMult = cellSizeUnitMultiplier * 0.1f;
        Vector3 scale = new Vector3(islandSize.x + 1, 1, islandSize.y + 1) * calculatedMult;
        scale.y = 1;
        ground.transform.localScale = scale;

        ChunkTransform[] chunkTransforms = BinaryArrayPartition.GetPartitionedChunks(islandSize.x, islandSize.y, () => { return Mathf.FloorToInt(AdvanceRandom.ExponentialRandom(maxChunkSizeLowerBound, expectedChunkSizeVariation)); });

        //VerifyChunkAvailability(requiredChunks, chunkTransforms);

        //Grab all requiredChunks before generation and add them in first.
        List<ChunkTransform> filledChunks = new();
        foreach (PCGChunkData requiredChunk in requiredChunks)
        {
            ChunkTransform chunkTransform;
            do
            {
                chunkTransform = chunkTransforms[Random.Range(0, chunkTransforms.Length)];
            } while (filledChunks.Contains(chunkTransform)); //keep retrying until unfilled chunk found.

            filledChunks.Add(chunkTransform);
            GenerateChunk(requiredChunk, chunkTransform);
        }

        //Fill in the rest of the chunks.
        foreach (ChunkTransform chunkTransform in chunkTransforms)
        {
            if(filledChunks.Contains(chunkTransform))
            {
                continue; //Skip if chunk is already filled.
            }
            GenerateChunk(availableChunks[Random.Range(0, availableChunks.Length)], chunkTransform);
        }
    }

    private void GenerateChunk(PCGChunkData chunkData, ChunkTransform chunkTransform)
    {
            //create a copy of chunk data so we don't modify the scriptable object on disk
            PCGChunkData chunkDataCopy = Instantiate(chunkData);

            //Initilize and generate chunk
            chunkDataCopy.Initilize(chunkTransform);
            GameObject chunkObj = chunkDataCopy.Generate(transform, cellSizeUnitMultiplier);
            
            //move chunk to new position
            chunkObj.transform.localPosition = GridPosToWorldV3(chunkTransform.ChunkCenter);
    }

    private bool VerifyChunkAvailability(PCGChunkData[] requiredChunks, ChunkTransform[] availableChunks)
    {
        //Fail if there aren't enough chunks
        if(availableChunks.Length < requiredChunks.Length)
            return false;

        //Verify there are enough chunk of required size
        List<int> availableChunkSizeCount = new();
        foreach(ChunkTransform ct in availableChunks)
        {
            if(availableChunkSizeCount.Count <= ct.ChunkCellCount)
            {
                availableChunkSizeCount.AddRange(new int[ct.ChunkCellCount - availableChunkSizeCount.Count + 1]);
            }
            availableChunkSizeCount[ct.ChunkCellCount] += 1;
        }

        List<int> requiredChunkSizeCount = new();
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
        return transform.position + new Vector3(xCord - (islandSize.x - 1) * 0.5f, 0, yCord - (islandSize.y - 1) * 0.5f) * cellSizeUnitMultiplier;
    }

    private Vector3 GridPosToWorldV3(Vector2 gridCord)
    {
        return transform.position + new Vector3(gridCord.x - (islandSize.x - 1) * 0.5f, 0, gridCord.y - (islandSize.y - 1) * 0.5f) * cellSizeUnitMultiplier;
    }
}
