using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGStructureModule : MonoBehaviour
{
    [field: SerializeField] public ChunkTransform.ChunkCellPosition modulePosition { get; private set; }
    [field: SerializeField] public bool walkable { get; private set; }
}
