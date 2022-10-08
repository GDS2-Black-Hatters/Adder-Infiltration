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
}
