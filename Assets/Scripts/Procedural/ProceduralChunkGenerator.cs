using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralChunkGenerator : PCGenerator
{
    [SerializeField] private PCGChunkDataBase chunkData;
    [SerializeField] private Vector2Int chunkSize;

    protected override IEnumerator Generate()
    {
        PCGChunkDataBase chunkDataCopy = Instantiate(chunkData);
        ChunkTransform chunkTransform = new(Vector2Int.zero, chunkSize - Vector2Int.one);
        
        if(!chunkDataCopy.CanGenerateInTransform(chunkTransform))
        {
            Debug.LogError("Chunk cannot generate with the given chunkSize.");
            yield break;
        }

        chunkDataCopy.Initilize(chunkTransform);
        Transform chunkBase = new GameObject("ChunkBase").transform;
        chunkBase.SetParent(transform);

        chunkDataCopy.Generate(chunkBase, new GameObject(), this, GenerationIncomplete);
        
        chunkBase.localPosition = Vector3.zero;
        chunkBase.localRotation = Quaternion.identity;
        yield break;
    }
}
