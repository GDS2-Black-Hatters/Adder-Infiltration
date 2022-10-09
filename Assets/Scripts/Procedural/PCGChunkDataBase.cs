using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCGChunkDataBase : PCGeneratableSO
{
    private const float chunkBoarderWidth = 0.5f;
    private const float chunkBoarderObjOffset = 0.05f;

    protected ChunkTransform chunkTransform;

    [SerializeField] private GameObject chunkBaseObjectPrefab;

    //[SerializeField] private BaseEnvironmentObject chunkBorderInnerObjects;
    [SerializeField] private RoadEnvObject[] chunkBorderRoadSideObjects;
    [SerializeField, Range(0,1)] private float outerBorderObjectSpawnChance;

    protected float cellUnitMultiplier;

    public void Initilize(ChunkTransform chunkTransform)
    {
        this.chunkTransform = chunkTransform;
    }

    public abstract bool CanGenerateInTransform(ChunkTransform chunkTransform);

    protected void GenerateBorderObjects(Transform parentTransform)
    {
        //X sides
        for(int x = chunkTransform.upperLeft.x; x <= chunkTransform.bottomRight.x; x++)
        {
            //Upper Edge
            if(chunkBorderRoadSideObjects.Length > 0 && DoStatic.RandomBool(outerBorderObjectSpawnChance))
            {
                RoadEnvObject newOuterObj = Instantiate(chunkBorderRoadSideObjects[Random.Range(0, chunkBorderRoadSideObjects.Length)], Vector3.zero, Quaternion.identity);
                float randomOffset = Random.Range(-(cellUnitMultiplier - newOuterObj.objectLength) * 0.5f, (cellUnitMultiplier - newOuterObj.objectLength) * 0.5f)/cellUnitMultiplier;
                newOuterObj.transform.localPosition = relativeCellPosition(x + randomOffset, chunkTransform.upperLeft.y - (0.5f + chunkBoarderWidth * 0.5f + chunkBoarderObjOffset));
                newOuterObj.transform.Rotate(newOuterObj.transform.up, 90);
                newOuterObj.transform.SetParent(parentTransform);
            }
            //Lower Edge
            if(chunkBorderRoadSideObjects.Length > 0 && DoStatic.RandomBool(outerBorderObjectSpawnChance))
            {
                RoadEnvObject newOuterObj = Instantiate(chunkBorderRoadSideObjects[Random.Range(0, chunkBorderRoadSideObjects.Length)], Vector3.zero, Quaternion.identity);
                float randomOffset = Random.Range(-(cellUnitMultiplier - newOuterObj.objectLength) * 0.5f, (cellUnitMultiplier - newOuterObj.objectLength) * 0.5f)/cellUnitMultiplier;
                newOuterObj.transform.localPosition = relativeCellPosition(x + randomOffset, chunkTransform.bottomRight.y + (0.5f + chunkBoarderWidth * 0.5f + chunkBoarderObjOffset));
                newOuterObj.transform.Rotate(newOuterObj.transform.up, 270);
                newOuterObj.transform.SetParent(parentTransform);
            }
        }

        //Y sides
        for(int y = chunkTransform.upperLeft.y; y <= chunkTransform.bottomRight.y; y++)
        {
            //Left Edge
            if(chunkBorderRoadSideObjects.Length > 0 && DoStatic.RandomBool(outerBorderObjectSpawnChance))
            {
                RoadEnvObject newOuterObj = Instantiate(chunkBorderRoadSideObjects[Random.Range(0, chunkBorderRoadSideObjects.Length)], Vector3.zero, Quaternion.identity);
                float randomOffset = Random.Range(-(cellUnitMultiplier - newOuterObj.objectLength) * 0.5f, (cellUnitMultiplier - newOuterObj.objectLength) * 0.5f)/cellUnitMultiplier;
                newOuterObj.transform.localPosition = relativeCellPosition(chunkTransform.upperLeft.x - (0.5f + chunkBoarderWidth * 0.5f + chunkBoarderObjOffset), y + randomOffset);
                newOuterObj.transform.Rotate(newOuterObj.transform.up, 180);
                newOuterObj.transform.SetParent(parentTransform);
            }
            //Right Edge
            if(chunkBorderRoadSideObjects.Length > 0 && DoStatic.RandomBool(outerBorderObjectSpawnChance))
            {
                RoadEnvObject newOuterObj = Instantiate(chunkBorderRoadSideObjects[Random.Range(0, chunkBorderRoadSideObjects.Length)], Vector3.zero, Quaternion.identity);
                float randomOffset = Random.Range(-(cellUnitMultiplier - newOuterObj.objectLength) * 0.5f, (cellUnitMultiplier - newOuterObj.objectLength) * 0.5f)/cellUnitMultiplier;
                newOuterObj.transform.localPosition = relativeCellPosition(chunkTransform.bottomRight.x + (0.5f + chunkBoarderWidth * 0.5f + chunkBoarderObjOffset), y + randomOffset);
                newOuterObj.transform.Rotate(newOuterObj.transform.up, 0f);
                newOuterObj.transform.SetParent(parentTransform);
            }
        }
    }

    protected Vector3 relativeCellPosition(float posX, float posY)
    {
        return new Vector3((posX - chunkTransform.ChunkCenter.x) * cellUnitMultiplier, 0, (posY - chunkTransform.ChunkCenter.y) * cellUnitMultiplier);
    }

    protected Transform InstantiateRootAndGround(Transform parentTransform)
    {
        Transform root = new GameObject(name).transform;
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
