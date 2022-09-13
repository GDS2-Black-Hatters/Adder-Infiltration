using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGIslandData : PCGeneratableSO
{
    [SerializeField] private PCGChunkData[] requiredChunks;
    [SerializeField] private PCGChunkData[] availableFreeUseChunks;

    public override GameObject Generate(Transform parentTransform)
    {
        throw new System.NotImplementedException();
    }
}
