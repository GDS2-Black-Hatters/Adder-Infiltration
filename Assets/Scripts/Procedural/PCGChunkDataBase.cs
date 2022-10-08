using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCGChunkDataBase : PCGeneratableSO
{
    private const float chunkBoarderWidth = 0.5f;

    protected ChunkTransform chunkTransform;

    [SerializeField] private GameObject chunkBaseObjectPrefab;

    protected float cellUnitMultiplier;

    public void Initilize(ChunkTransform chunkTransform)
    {
        this.chunkTransform = chunkTransform;
    }

    public abstract bool CanGenerateInTransform(ChunkTransform chunkTransform);

    protected Vector3 relativeCellPosition(float posX, float posY)
    {
        return new Vector3((posX - chunkTransform.ChunkCenter.x) * cellUnitMultiplier, 0, (posY - chunkTransform.ChunkCenter.y) * cellUnitMultiplier);
    }

    protected Transform InstantiateRootAndGround(Transform parentTransform)
    {
        Transform root = new GameObject("chunk").transform;
        root.SetParent(parentTransform);
        root.localPosition = Vector3.zero;

        GameObject chunkBase = Instantiate(chunkBaseObjectPrefab, Vector3.zero, Quaternion.identity, root);
        chunkBase.transform.localScale = Vector3.Scale(chunkBase.transform.localScale, new Vector3(chunkTransform.ChunkWidth + chunkBoarderWidth, 1, chunkTransform.ChunkHeight + chunkBoarderWidth));

        return root;
    }

    protected int GetChunkModuleRotateMultiplier(Vector2Int cellCord, int defaultReturn = 0)
    {
        // 2221
        // 3XX1
        // 3XX1
        // 3000
        if(cellCord.y == chunkTransform.bottomRight.y && cellCord.x != chunkTransform.upperLeft.x)
            return 0;

        if(cellCord.x == chunkTransform.bottomRight.x && cellCord.y != chunkTransform.bottomRight.y)
            return 1;
        
        if(cellCord.y == chunkTransform.upperLeft.y && cellCord.x != chunkTransform.bottomRight.x)
            return 2;
        
        if(cellCord.x == chunkTransform.upperLeft.x && cellCord.y != chunkTransform.upperLeft.y)
            return 3;
        
        return defaultReturn;
    }
}
