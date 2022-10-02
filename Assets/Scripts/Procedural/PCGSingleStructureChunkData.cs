using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chunk/SingleStructureChunk", fileName = "NewSingleStructureChunk")]
public class PCGSingleStructureChunkData : PCGChunkDataBase
{
    [SerializeField] private PCGStructureModule[] structureModules;

    public override GameObject Generate(Transform parentTransform, float cellUnitMultiplier)
    {
        this.cellUnitMultiplier = cellUnitMultiplier;

        Transform root = InstantiateRootAndGround(parentTransform);


        //Assign the modules into lists
        List<PCGStructureModule> cornerModules = new List<PCGStructureModule>();
        List<PCGStructureModule> sideModules = new List<PCGStructureModule>();
        List<PCGStructureModule> centerModules = new List<PCGStructureModule>();

        foreach (PCGStructureModule module in structureModules)
        {
            switch (module.modulePosition)
            {   
                case ChunkTransform.ChunkCellPosition.corner:
                cornerModules.Add(module);
                break;

                case ChunkTransform.ChunkCellPosition.edge:
                sideModules.Add(module);
                break;

                case ChunkTransform.ChunkCellPosition.center:
                centerModules.Add(module);
                break;

                default:
                Debug.LogWarning("Module position not assigned for module: " + module.name);
                break;
            }
        }

        ref List<PCGStructureModule> activeModuleList = ref centerModules;
        for(int x = chunkTransform.upperLeft.x; x <= chunkTransform.bottomRight.x; x++)
        {
            for(int y = chunkTransform.upperLeft.y; y <= chunkTransform.bottomRight.y; y++)
            {
                switch(CellCordToPosition(new( x, y )))
                {
                    case ChunkTransform.ChunkCellPosition.corner:
                    activeModuleList = ref cornerModules;
                    break;

                    case ChunkTransform.ChunkCellPosition.edge:
                    activeModuleList = ref sideModules;
                    break;

                    case ChunkTransform.ChunkCellPosition.center:
                    default:
                    activeModuleList = ref centerModules;
                    break;
                }

                PopulateCell(activeModuleList[Random.Range(0, activeModuleList.Count)], new(x,y), GetChunkBorderModuleRotateMultiplier(new(x,y)), root);
            }
        }

        return root.gameObject;
    }

    private GameObject PopulateCell(PCGStructureModule fillContentBlock, Vector2 fillCellCord, int rotationMultiplier, Transform chunkBaseTransform)
    {
        PCGStructureModule newStructure = Instantiate<PCGStructureModule>(fillContentBlock);
        newStructure.transform.position = relativeCellPosition(fillCellCord.x, fillCellCord.y);
        newStructure.transform.Rotate(Vector3.up, 90 * rotationMultiplier);
        newStructure.transform.SetParent(chunkBaseTransform);
        return newStructure.gameObject;
    }

    private ChunkTransform.ChunkCellPosition CellCordToPosition(Vector2Int cellCord)
    {
        bool xMatch = false;
        bool yMatch = false;

        if(cellCord.x == chunkTransform.upperLeft.x || cellCord.x == chunkTransform.bottomRight.x)
        {
            xMatch = true;
        }

        if(cellCord.y == chunkTransform.upperLeft.y || cellCord.y == chunkTransform.bottomRight.y)
        {
            yMatch = true;
        }

        if(xMatch && yMatch) return ChunkTransform.ChunkCellPosition.corner;
        if(xMatch || yMatch) return ChunkTransform.ChunkCellPosition.edge;

        return ChunkTransform.ChunkCellPosition.center;
    }

    private int GetChunkBorderModuleRotateMultiplier(Vector2Int cellCord)
    {
        // 2221
        // 3XX1
        // 3XX1
        // 3000
        if(cellCord.x == chunkTransform.bottomRight.x && cellCord.y != chunkTransform.bottomRight.y)
            return 1;
        
        if(cellCord.y == chunkTransform.upperLeft.y && cellCord.x != chunkTransform.bottomRight.x)
            return 2;
        
        if(cellCord.x == chunkTransform.upperLeft.x && cellCord.y != chunkTransform.upperLeft.y)
            return 3;
        
        return 0;
    }

    public override bool CanGenerateInTransform(ChunkTransform chunkTransform)
    {
        if(chunkTransform.ChunkWidth < 2 || chunkTransform.ChunkHeight < 2)
        {
            return false;
        }
        
        return true;
    }
}
