using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralChunkGenerator : MonoBehaviour
{
    [SerializeField] private PCGChunkDataBase chunkData;
    [SerializeField] private Vector2Int chunkSize;
    private const float cellSizeMultiplier = 25;

    void Start()
    {
        PCGChunkDataBase chunkDataCopy = Instantiate(chunkData);
        ChunkTransform chunkTransform = new(Vector2Int.zero, chunkSize - Vector2Int.one);
        
        if(!chunkDataCopy.CanGenerateInTransform(chunkTransform))
        {
            Debug.LogError("Chunk cannot generate with the given chunkSize.");
            Destroy(this);
            return;
        }

        chunkDataCopy.Initilize(chunkTransform);
        Transform chunkBase = new GameObject("ChunkBase").transform;
        chunkBase.SetParent(transform);

        chunkDataCopy.Generate(chunkBase, cellSizeMultiplier);
        
        chunkBase.localPosition = Vector3.zero;
        chunkBase.localRotation = Quaternion.identity;
        Destroy(this);        
    }
}
