using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chunk", fileName = "NewChunk")]
public class PCGChunkData : PCGeneratableSO
{
    private const float chunkBoarderWidth = 0.5f;

    [SerializeField] private GameObject chunkBaseObjectPrefab;

    [SerializeField] private int _minCellCountRequirement;
    public int minCellCountRequirement { get { return _minCellCountRequirement; }}
    [SerializeField] private PCGBlockScriptable[] requiredBlocks;
    [SerializeField] private PCGBlockScriptable[] availableFreeBlocks;

    private ChunkTransform chunkTransform;
    private float cellUnitMultiplier;

    public void Initilize(ChunkTransform chunkTransform)
    {
        this.chunkTransform = chunkTransform;
    }

    public override GameObject Generate(Transform parentTransform, float cellUnitMultiplier)
    {
        this.cellUnitMultiplier = cellUnitMultiplier;

        Transform root = new GameObject("chunk").transform;
        root.SetParent(parentTransform);
        root.localPosition = Vector3.zero;

        //Generate Chunk Floor
        GameObject chunkBase = Instantiate(chunkBaseObjectPrefab, Vector3.zero, Quaternion.identity, root);
        chunkBase.transform.localScale = Vector3.Scale(chunkBase.transform.localScale, new Vector3(chunkTransform.ChunkWidth + chunkBoarderWidth, 1, chunkTransform.ChunkHeight + chunkBoarderWidth));

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

    private Vector3 relativeCellPosition(float posX, float posY)
    {
        return new Vector3((posX - chunkTransform.ChunkCenter.x) * cellUnitMultiplier, 0, (posY - chunkTransform.ChunkCenter.y) * cellUnitMultiplier);
    }
}
