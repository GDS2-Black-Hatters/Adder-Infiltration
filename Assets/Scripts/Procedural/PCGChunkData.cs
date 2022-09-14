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
    private float cellSizeUnitMultiplier;

    public void Initilize(ChunkTransform chunkTransform, float cellSizeMultiplier)
    {
        this.chunkTransform = chunkTransform;
        this.cellSizeUnitMultiplier = cellSizeMultiplier;
    }

    public override GameObject Generate(Transform parentTransform)
    {
        //Generate Chunk Floor
        GameObject chunkBase = Instantiate(chunkBaseObjectPrefab, Vector3.zero, Quaternion.identity, parentTransform);
        chunkBase.transform.localScale = Vector3.Scale(chunkBase.transform.localScale, new Vector3(chunkTransform.ChunkWidth + chunkBoarderWidth, 1, chunkTransform.ChunkHeight + chunkBoarderWidth));

        //Fill Chunk with content
        for(int i = chunkTransform.upperLeft.x; i <= chunkTransform.bottomRight.x; i++)
        {   
            for(int j = chunkTransform.upperLeft.y; j <= chunkTransform.bottomRight.y; j++)
            {
                GameObject newStructure = availableFreeBlocks[Random.Range(0, availableFreeBlocks.Length)].Generate(chunkBase.transform);
                newStructure.transform.position = relativeCellPosition(i, j);
                newStructure.transform.SetParent(chunkBase.transform);
            }
        }

        /*
        for(int i = chunk.upperLeft.x; i <= chunk.bottomRight.x; i++)
        {   
            for(int j = chunk.upperLeft.y; j <= chunk.bottomRight.y; j++)
            {
                GameObject newStructure = Instantiate(cellFillerPrefab, GridPosToWorldV3(i, j), Quaternion.identity);
                newStructure.transform.SetParent(chunkFloor.transform);
            }
        }
        */

        return chunkBase;
    }

    private Vector3 relativeCellPosition(float posX, float posY)
    {
        return new Vector3((posX - chunkTransform.ChunkCenter.x) * cellSizeUnitMultiplier, 0, (posY - chunkTransform.ChunkCenter.y) * cellSizeUnitMultiplier);
    }
}
