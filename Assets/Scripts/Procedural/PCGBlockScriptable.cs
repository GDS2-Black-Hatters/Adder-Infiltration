using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCGBlockScriptable : PCGeneratableSO
{
    public ChunkTransform.ChunkCellPosition possibleCellPosition = ~(ChunkTransform.ChunkCellPosition)0;  //NOT nothing -> Yes everything
    public bool doesNotBlockPathing = false;
    public abstract Vector2Int blockSize { get; } //Currently not actively used, this is only added to ease future modifications to the generation algorythm.
}
