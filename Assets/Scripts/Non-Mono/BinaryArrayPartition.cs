using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinaryArrayPartition
{
    private enum cardinal
    { posX, negX, posY, negY}

    private enum lineDirection
    { alongX, alongY}

    public struct ChunkInfo
    {
        public ChunkInfo(Vector2Int upperLeft, Vector2Int bottomRight)
        {
            this.upperLeft = upperLeft;
            this.bottomRight = bottomRight;
        }

        public Vector2Int upperLeft;
        public Vector2Int bottomRight;

        public int ChunkWidth
        {
            get { return bottomRight.x - upperLeft.x + 1; }
        }

        public int ChunkHeight
        {
            get { return bottomRight.y - upperLeft.y + 1; }
        }

        public int ChunkSize
        {
            get { return ChunkWidth * ChunkHeight; }
        }

        public bool IsWithinChunk(Vector2Int position)
        {
            return !(position.x < upperLeft.x || position.x > bottomRight.x || position.y < upperLeft.y || position.y > bottomRight.y) ;
        }
    }

    private class ChunkGraphNode
    {
        public ChunkGraphNode(Vector2Int upperLeft, Vector2Int bottomRight)
        {
            chunkInfo.upperLeft = upperLeft;
            chunkInfo.bottomRight = bottomRight;
        }

        public ChunkInfo chunkInfo;
        //this overall assums if one child exist, the other will, due to how the graph will be constructed.
        public ChunkGraphNode leftChild;
        public ChunkGraphNode rightChild;

        public bool IsEndChunkNode
        {
            get { return leftChild == null; }
        }

        public bool IsWithinEndChunkNode(Vector2Int position)
        {
            //Debug.Log("Chunk: UL:" + this.upperLeft + ",BR:" + this.bottomRight + ", CheckPos:" + position);

            //Return False if position is not within this chunk, the position is likely a boundary position, or belongs to the other path.
            if(!chunkInfo.IsWithinChunk(position))
            {
                return false;
            }
            if(IsEndChunkNode)
            {
                return true;
            }
            return( leftChild.IsWithinEndChunkNode(position) || rightChild.IsWithinEndChunkNode(position));
        }

        public ChunkInfo[] GetEndChunksInfo()
        {
            List<ChunkInfo> returnInfoList = new();
            GetEndChunksInfo(ref returnInfoList);
            return returnInfoList.ToArray();
        }
    
        private void GetEndChunksInfo(ref List<ChunkInfo> refList)
        {
            if(IsEndChunkNode)
            {
                refList.Add(chunkInfo);
                return;
            }
            leftChild.GetEndChunksInfo(ref refList);
            rightChild.GetEndChunksInfo(ref refList);
        }
    }

    /// <summary>
    /// Modifies the array by replacing some of the cells as 'boundary cell'
    /// Expects a fully 'filled' array as a ref input where none of the cells are 'boundary' cell
    /// Will return an array of chunk info containing all non boundary chunks' cooridinates.
    /// </summary>
    public static ChunkInfo[] PartitionArray<T>(ref T[,] array, T boundaryCell, System.Func<T, bool> IsBoundaryCompare, System.Func<int> maxChunkSizeFunc)
    {
        ChunkGraphNode rootGraphNode = CreatePartitionGraph(new(array.GetLength(0), array.GetLength(1)), maxChunkSizeFunc);

        for(int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if(rootGraphNode.IsWithinEndChunkNode(new Vector2Int(i, j)))
                {
                    array[i,j] = boundaryCell;
                }
            }    
        }

        return rootGraphNode.GetEndChunksInfo();
    }

    /// <summary>
    /// Generates chunks using binary space partition concept with gaps between chunks.
    /// Does the same thing as PartitionArray but will be faster is no array is needed.
    /// </summary>
    public static ChunkInfo[] GetPartitionedChunks(int mapWidth, int mapHeight, System.Func<int> maxChunkSizeFunc)
    {
        ChunkGraphNode rootGraphNode = CreatePartitionGraph(new(mapWidth, mapHeight), maxChunkSizeFunc);

        return rootGraphNode.GetEndChunksInfo();
    }

    /// <summary>
    /// Creates a node graph where end nodes will be individual chunks of land
    /// </summary>
    private static ChunkGraphNode CreatePartitionGraph(Vector2Int graphSize, System.Func<int> maxChunkSizeFunc)
    {
        ChunkGraphNode chunkInfo = new(Vector2Int.zero, graphSize - Vector2Int.one);

        //perform chunk partition
        RecursivePartitionChunk(ref chunkInfo, maxChunkSizeFunc);

        return chunkInfo;
    }


    /// <summary>
    /// Recursively Partition Chunks into smaller sub-chunks as childs
    /// </summary>
    private static void RecursivePartitionChunk(ref ChunkGraphNode chunkNode, System.Func<int> maxChunkSizeFunc)
    {
        //Return if chunk is small enough or cannot be divided furthur
        if(chunkNode.chunkInfo.ChunkSize < maxChunkSizeFunc.Invoke() || (chunkNode.chunkInfo.ChunkWidth < 3 && chunkNode.chunkInfo.ChunkHeight < 3))
        {
            return;
        }
        PartitionChunk(ref chunkNode);
        RecursivePartitionChunk(ref chunkNode.leftChild, maxChunkSizeFunc);
        RecursivePartitionChunk(ref chunkNode.rightChild, maxChunkSizeFunc);
    }

    private static void PartitionChunk(ref ChunkGraphNode chunkToPart)
    {
        lineDirection partDirection;
        ref ChunkInfo chunkInfo = ref chunkToPart.chunkInfo;

        //Find the direction to part the chunk
        if(chunkInfo.ChunkWidth < 3)
        {
            partDirection = lineDirection.alongX;
        }
        else if(chunkInfo.ChunkHeight < 3)
        {
            partDirection = lineDirection.alongY;
        }
        else
        {
            //select direction randomly weighted in advantage to split the 'longer' side
            partDirection = DoStatic.RandomBool(chunkInfo.ChunkHeight/(float)(chunkInfo.ChunkHeight + chunkInfo.ChunkWidth)) ? lineDirection.alongX : lineDirection.alongY;
        }

        //Perform the partition, use GaussianRandom so the chunk is more likely to be split near the center
        const float centeringPower = 0.2f;
        int splitPosition;
        switch(partDirection)
        {
            case lineDirection.alongX:
            splitPosition = Mathf.Clamp(Mathf.RoundToInt(AdvanceRandom.GaussianRandom( (chunkInfo.bottomRight.y + chunkInfo.upperLeft.y)/2, Mathf.Pow(chunkInfo.ChunkHeight, centeringPower))), chunkInfo.upperLeft.y + 1, chunkInfo.bottomRight.y - 1);
            chunkToPart.leftChild = new(new(chunkInfo.upperLeft.x, chunkInfo.upperLeft.y), new(chunkInfo.bottomRight.x, splitPosition - 1));
            chunkToPart.rightChild = new(new(chunkInfo.upperLeft.x, splitPosition + 1), new(chunkInfo.bottomRight.x, chunkInfo.bottomRight.y));
            //Debug.Log("Chunk: UL:" + chunkToPart.upperLeft + ",BR:" + chunkToPart.bottomRight + ", SplitYPos:" + splitPosition);
            break;

            case lineDirection.alongY:
            splitPosition = Mathf.Clamp(Mathf.RoundToInt(AdvanceRandom.GaussianRandom( (chunkInfo.bottomRight.x + chunkInfo.upperLeft.x)/2, Mathf.Pow(chunkInfo.ChunkWidth, centeringPower))), chunkInfo.upperLeft.x + 1, chunkInfo.bottomRight.x - 1);
            chunkToPart.leftChild = new(new(chunkInfo.upperLeft.x, chunkInfo.upperLeft.y), new(splitPosition - 1, chunkInfo.bottomRight.y));
            chunkToPart.rightChild = new(new(splitPosition + 1, chunkInfo.upperLeft.y), new(chunkInfo.bottomRight.x, chunkInfo.bottomRight.y));
            //Debug.Log("Chunk: UL:" + chunkToPart.upperLeft + ",BR:" + chunkToPart.bottomRight + ", SplitXPos:" + splitPosition);
            break;
        }
    }
}
