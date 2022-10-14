using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chunk/RandomCellFillChunk", fileName = "NewCellFillChunk")]
public class PCGRandomCellChunkData : PCGChunkDataBase
{
    [SerializeField] private PCGBlockScriptable[] requiredBlocks;
    [SerializeField] private PCGBlockScriptable[] availableFreeBlocks;

    [SerializeField] private float randomWalkableTileChance = 0.3f;

    public override GameObject Generate(Transform parentTransform)
    {
        Transform root = InstantiateRootAndGround(parentTransform);

        bool[,] tileWalkable = new bool[chunkTransform.ChunkWidth, chunkTransform.ChunkHeight];

        int xi = 0, yi = 1;
        int xj = chunkTransform.ChunkWidth - 1;
        int yj = chunkTransform.ChunkHeight - 1;

        int x = 0, y = 0;
        bool shiftingX = true;
        bool incrimenting = true;

        bool incriment() //return true when no more increments are possible, i.e. end of loop
        {
            bool done = false;

            /* Should create a loop like this
            0  1  2  3
            11 12 13 4
            10 15 14 5
            9  8  7  6
            */
            while(!done)
            {
                if(shiftingX)
                {
                    if(incrimenting)
                    {
                        if(x == xj) // hit right edge
                        {
                            if (yi > yj)
                            {
                                return true; //end of loop
                            }
                            xj--;
                            shiftingX = false;
                            continue;
                        }
                        x++;
                        done = true;
                    }
                    else
                    {
                        if(x == xi) // hit left edge
                        {
                            if (yi > yj)
                            {
                                return true; //end of loop
                            }
                            xi++;
                            shiftingX = false;
                            continue;
                        }
                        x--;
                        done = true;
                    }
                }
                else //Shifting Y
                {
                    if (incrimenting)
                    {
                        if(y == yj) // hit bottom edge
                        {
                            if(xi > xj)
                            {
                                return true; //end of loop
                            }
                            yj--;
                            shiftingX = true;
                            incrimenting = false;
                            continue;
                        }
                        y++;
                        done = true;
                    }
                    else
                    {
                        if(y == yi) // hit top edge
                        {
                            if(xi > xj)
                            {
                                return true; //end of loop
                            }
                            yi++;
                            shiftingX = true;
                            incrimenting = true;
                            continue;
                        }
                        y--;
                        done = true;
                    }
                }

            }
            return false;
        }

        int walkableCount = 0;
        do {
            //do stuff
            //Debug.Log("X:" + x + " Y:" + y);
            tileWalkable[x,y] = tileIsReachable(new(x,y), tileWalkable) && DoStatic.RandomBool(randomWalkableTileChance);
            walkableCount += tileWalkable[x,y] ? 1 : 0;
        } while (!incriment());

        int requiredWalkable = 0;
        foreach (PCGBlockScriptable block in requiredBlocks)
        {
            requiredWalkable += block.doesNotBlockPathing ? 1 : 0;
        }

        //force invert some walkable tiles if needed
        int requiredConversion = requiredWalkable - walkableCount;
        while (requiredConversion > 0)
        {
            x = Random.Range(0, tileWalkable.GetLength(0));            
            y = Random.Range(0, tileWalkable.GetLength(1));            
            if(!tileWalkable[x,y] && tileIsReachable(new(x,y), tileWalkable))
            {
                tileWalkable[x,y] = true;
                requiredConversion--;
            }
        }

        //keep a list of V2Int of filled cells
        List<Vector2Int> cellFillCheck = new List<Vector2Int>();

        //Prioritise randomly filling in required blocks first
        List<PCGBlockScriptable> requiredBlockList = new(requiredBlocks);
        const int maxAttempt = 99;
        int attempt = 0;
        while(requiredBlockList.Count > 0)
        {
            if(attempt>maxAttempt)
            {
                Debug.LogError("Unable to find suitable cell for required block: " + requiredBlockList[0] + ", Ignoring.");
                attempt = 0;
                requiredBlockList.RemoveAt(0);
                continue;
            }

            Vector2Int fillCellV2I = new(Random.Range(chunkTransform.upperLeft.x, chunkTransform.bottomRight.x + 1), Random.Range(chunkTransform.upperLeft.y, chunkTransform.bottomRight.y + 1));
            if (cellFillCheck.Contains(fillCellV2I) || tileWalkable[fillCellV2I.x - chunkTransform.upperLeft.x, fillCellV2I.y - chunkTransform.upperLeft.y] != requiredBlockList[0].doesNotBlockPathing)
            {
                attempt++;
                continue; //ignore attempt and retry if cell is already filled, or if the walkable comparison fails.
            }
            PopulateCell(requiredBlockList[0], fillCellV2I, GetChunkModuleRotateMultiplier(fillCellV2I), root);
            requiredBlockList.RemoveAt(0);
            attempt = 0;
            cellFillCheck.Add(fillCellV2I);
        }

        //Fill Chunk with content
        List<PCGBlockScriptable> usableBlock = new List<PCGBlockScriptable>();
        for(int i = chunkTransform.upperLeft.x; i <= chunkTransform.bottomRight.x; i++)
        {   
            for(int j = chunkTransform.upperLeft.y; j <= chunkTransform.bottomRight.y; j++)
            {
                usableBlock.Clear();
                if(cellFillCheck.Contains(new(i,j)))
                    continue; //skip fill cell if cell is already populated
                foreach (PCGBlockScriptable block in availableFreeBlocks)
                {
                    if(block.doesNotBlockPathing == tileWalkable[i - chunkTransform.upperLeft.x,j - chunkTransform.upperLeft.y])
                    {
                        usableBlock.Add(block);
                    }
                }
                if(usableBlock.Count == 0)
                {
                    Debug.LogError("No usable block in chunk (" + name + ") with condition: Walkable:" + tileWalkable[i - chunkTransform.upperLeft.x,j - chunkTransform.upperLeft.y]);
                    continue;
                }
                PopulateCell(usableBlock[Random.Range(0, usableBlock.Count)], new(i,j), GetChunkModuleRotateMultiplier(new(i,j)), root);
            }
        }

        GenerateBorderObjects(root);

        return root.gameObject;
    }

    private bool tileIsReachable(Vector2Int tileCord, bool[,] tileWalkableStateArray)
    {
        if(tileCord.x == 0 || tileCord.y == 0 || tileCord.x == tileWalkableStateArray.GetLength(0) - 1 || tileCord.y == tileWalkableStateArray.GetLength(1) - 1)//Tile is on the edge
        {
            return true;
        }

        //Return true if any of the 4 neighbour is marked reachable
        //Should not go out of bounds since we have confirmed our position is not on the array boundaries above
        return (tileWalkableStateArray[tileCord.x - 1, tileCord.y] || tileWalkableStateArray[tileCord.x + 1, tileCord.y] || tileWalkableStateArray[tileCord.x, tileCord.y - 1] || tileWalkableStateArray[tileCord.x, tileCord.y + 1]);
    }

    private GameObject PopulateCell(PCGBlockScriptable fillContentBlock, Vector2 fillCellCord, int rotationMultiplier, Transform chunkBaseTransform)
    {
        GameObject newStructure = fillContentBlock.Generate(chunkBaseTransform);
        newStructure.transform.position = relativeCellPosition(fillCellCord.x, fillCellCord.y);
        newStructure.transform.Rotate(Vector3.up, rotationMultiplier * 90);
        newStructure.transform.SetParent(chunkBaseTransform);
        return newStructure;
    }

    public override bool CanGenerateInTransform(ChunkTransform chunkTransform)
    {
        return chunkTransform.ChunkCellCount > requiredBlocks.Length;
    }
}
