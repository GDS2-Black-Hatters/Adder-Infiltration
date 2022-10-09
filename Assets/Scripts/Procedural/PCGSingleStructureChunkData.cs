using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chunk/SingleStructureChunk", fileName = "NewSingleStructureChunk")]
public class PCGSingleStructureChunkData : PCGChunkDataBase
{
    [SerializeField] private PCGStructureModule[] structureModules;
    [SerializeField] private float generateThroughPathChance = 0;

    private class throughPathInfo
    {
        public throughPathInfo(SnapAxis axis, int cord)
        {
            this.axis = axis;
            this.cord = cord;
        }

        public SnapAxis axis { get; private set; }
        public int cord {get; private set; }

        public bool isPartOfThroughPath(Vector2Int v2i)
        {
            if(axis == SnapAxis.X)
            {
                return v2i.y == cord;
            }
            if(axis == SnapAxis.Y)
            {
                return v2i.x == cord;
            }
            return false;
        }
    }

    public override GameObject Generate(Transform parentTransform, float cellUnitMultiplier)
    {
        this.cellUnitMultiplier = cellUnitMultiplier;

        Transform root = InstantiateRootAndGround(parentTransform);

        //Assign the modules into lists
        List<PCGStructureModule> cornerModules = new List<PCGStructureModule>();
        List<PCGStructureModule> sideModules = new List<PCGStructureModule>();
        List<PCGStructureModule> sideThroughModules = new List<PCGStructureModule>();
        List<PCGStructureModule> centerModules = new List<PCGStructureModule>();
        List<PCGStructureModule> centerThroughModules = new List<PCGStructureModule>();

        foreach (PCGStructureModule module in structureModules)
        {
            List<PCGStructureModule> assignList = centerModules;
    
            switch (module.modulePosition)
            {   
                case ChunkTransform.ChunkCellPosition.corner:
                assignList = cornerModules;
                break;

                case ChunkTransform.ChunkCellPosition.edge:
                assignList = module.walkable ? sideThroughModules : sideModules;
                break;

                case ChunkTransform.ChunkCellPosition.center:
                assignList = module.walkable ? centerThroughModules : centerModules;
                break;

                default:
                Debug.LogWarning("Module position not assigned for module: " + module.name);
                continue;
            }
            assignList.Add(module);
        }

        throughPathInfo throughPath = null;
        if((chunkTransform.ChunkWidth >= 3 || chunkTransform.ChunkHeight >= 3) && DoStatic.RandomBool(generateThroughPathChance))
        {
            SnapAxis alongAxis;
            if(chunkTransform.ChunkWidth < 3)
            {
                alongAxis = SnapAxis.X;
            }
            else if(chunkTransform.ChunkHeight < 3)
            {
                alongAxis = SnapAxis.Y;
            }
            else
            {
                //select direction randomly weighted in advantage to put a path perpendicular to the 'longer' side
                //added in first order smoothstep to furthur increase bias
                float x = chunkTransform.ChunkHeight/(float)(chunkTransform.ChunkHeight + chunkTransform.ChunkWidth);
                alongAxis = DoStatic.RandomBool(x * x * x * (x * (x * 6 - 15) + 10)) ? SnapAxis.X : SnapAxis.Y;
            }

            int cord;
            float centeringPower = 0.3f;
            if(alongAxis == SnapAxis.X)
            {
                cord = Mathf.Clamp(Mathf.RoundToInt(AdvanceRandom.GaussianRandom( chunkTransform.ChunkCenter.y, Mathf.Pow(chunkTransform.ChunkHeight, centeringPower))), chunkTransform.upperLeft.y + 1, chunkTransform.bottomRight.y - 1);
            }
            else
            {
                cord = Mathf.Clamp(Mathf.RoundToInt(AdvanceRandom.GaussianRandom( chunkTransform.ChunkCenter.x, Mathf.Pow(chunkTransform.ChunkWidth, centeringPower))), chunkTransform.upperLeft.x + 1, chunkTransform.bottomRight.x - 1);
            }

            // Debug.Log("Width: " + chunkTransform.ChunkWidth + " Height: " + chunkTransform.ChunkHeight);
            // Debug.Log("Left: " + chunkTransform.upperLeft + " Right: " + chunkTransform.bottomRight);
            // Debug.Log("Axis: " + alongAxis + " Cord: " + cord);

            throughPath = new(alongAxis, cord);
        }

        List<PCGStructureModule> activeModuleList = centerModules;
        for(int x = chunkTransform.upperLeft.x; x <= chunkTransform.bottomRight.x; x++)
        {
            for(int y = chunkTransform.upperLeft.y; y <= chunkTransform.bottomRight.y; y++)
            {
                switch(CellCordToPosition(new( x, y )))
                {
                    case ChunkTransform.ChunkCellPosition.corner:
                    activeModuleList = cornerModules;
                    break;

                    case ChunkTransform.ChunkCellPosition.edge:
                    activeModuleList = (throughPath != null && throughPath.isPartOfThroughPath(new(x, y))) ? sideThroughModules : sideModules;
                    break;

                    case ChunkTransform.ChunkCellPosition.center:
                    default:
                    activeModuleList = (throughPath != null && throughPath.isPartOfThroughPath(new(x, y))) ? centerThroughModules : centerModules;
                    break;
                }

                PopulateCell(activeModuleList[Random.Range(0, activeModuleList.Count)], new(x,y), GetChunkModuleRotateMultiplier(new(x,y), throughPath != null ? throughPath.axis : SnapAxis.None), root);
            }
        }

        GenerateBorderObjects(root);

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

    private int GetChunkModuleRotateMultiplier(Vector2Int cellCord, SnapAxis throughAxis)
    {
        return(GetChunkModuleRotateMultiplier(cellCord, throughAxis == SnapAxis.X ? 1 : 0));
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
