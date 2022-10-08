using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chunk/RandomCellFillChunk", fileName = "NewCellFillChunk")]
public class PCGRandomCellChunkData : PCGChunkDataBase
{
    [SerializeField] private PCGBlockScriptable[] requiredBlocks;
    [SerializeField] private PCGBlockScriptable[] availableFreeBlocks;

    public override GameObject Generate(Transform parentTransform, float cellUnitMultiplier)
    {
        this.cellUnitMultiplier = cellUnitMultiplier;

        Transform root = InstantiateRootAndGround(parentTransform);

        //keep a list of V2Int of filled cells
        List<Vector2Int> cellFillCheck = new List<Vector2Int>();

        //Prioritise randomly filling in required blocks first
        List<PCGBlockScriptable> requiredBlockList = new(requiredBlocks);
        while(requiredBlockList.Count > 0)
        {
            Vector2Int fillCellV2I = new(Random.Range(chunkTransform.upperLeft.x, chunkTransform.bottomRight.x + 1), Random.Range(chunkTransform.upperLeft.y, chunkTransform.bottomRight.y + 1));
            if(cellFillCheck.Contains(fillCellV2I))
                continue; //ignore attempt and retry if cell is already filled.
            PopulateCell(requiredBlockList[0], fillCellV2I, root);
            requiredBlockList.RemoveAt(0);
            cellFillCheck.Add(fillCellV2I);
        }

        //Fill Chunk with content
        for(int i = chunkTransform.upperLeft.x; i <= chunkTransform.bottomRight.x; i++)
        {   
            for(int j = chunkTransform.upperLeft.y; j <= chunkTransform.bottomRight.y; j++)
            {
                if(cellFillCheck.Contains(new(i,j)))
                    continue; //skip fill cell if cell is already populated
                PopulateCell(availableFreeBlocks[Random.Range(0, availableFreeBlocks.Length)], new(i,j), root);
            }
        }

        return root.gameObject;
    }

    private GameObject PopulateCell(PCGBlockScriptable fillContentBlock, Vector2 fillCellCord, Transform chunkBaseTransform)
    {
        GameObject newStructure = fillContentBlock.Generate(chunkBaseTransform, cellUnitMultiplier);
        newStructure.transform.position = relativeCellPosition(fillCellCord.x, fillCellCord.y);
        newStructure.transform.SetParent(chunkBaseTransform);
        return newStructure;
    }

    public override bool CanGenerateInTransform(ChunkTransform chunkTransform)
    {
        return true;
    }
}
