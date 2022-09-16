using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinaryArrayPartition
{
    private enum cardinal
    { posX, negX, posY, negY}

    private enum lineDirection
    { alongX, alongY}

    private class ChunkGraphNode
    {
        public ChunkGraphNode(Vector2Int upperLeft, Vector2Int bottomRight)
        {
            chunkTransform = new( upperLeft, bottomRight);
        }

        public ChunkTransform chunkTransform;
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
            if(!chunkTransform.IsWithinChunk(position))
            {
                return false;
            }
            if(IsEndChunkNode)
            {
                return true;
            }
            return( leftChild.IsWithinEndChunkNode(position) || rightChild.IsWithinEndChunkNode(position));
        }

        public ChunkTransform[] GetEndChunksTransform()
        {
            List<ChunkTransform> returnTransformList = new();
            GetEndChunksTransform(ref returnTransformList);
            return returnTransformList.ToArray();
        }
    
        private void GetEndChunksTransform(ref List<ChunkTransform> refList)
        {
            if(IsEndChunkNode)
            {
                refList.Add(chunkTransform);
                return;
            }
            leftChild.GetEndChunksTransform(ref refList);
            rightChild.GetEndChunksTransform(ref refList);
        }
    }

    /// <summary>
    /// Modifies the array by replacing some of the cells as 'boundary cell'
    /// Expects a fully 'filled' array as a ref input where none of the cells are 'boundary' cell
    /// Will return an array of chunk transform containing all non boundary chunks' cooridinates.
    /// </summary>
    public static ChunkTransform[] PartitionArray<T>(ref T[,] array, T boundaryCell, System.Func<int> maxChunkSizeFunc)
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

        return rootGraphNode.GetEndChunksTransform();
    }

    /// <summary>
    /// Generates chunks using binary space partition concept with gaps between chunks.
    /// Does the same thing as PartitionArray but will be faster is no array is needed.
    /// </summary>
    public static ChunkTransform[] GetPartitionedChunks(int mapWidth, int mapHeight, System.Func<int> maxChunkSizeFunc)
    {
        ChunkGraphNode rootGraphNode = CreatePartitionGraph(new(mapWidth, mapHeight), maxChunkSizeFunc);

        return rootGraphNode.GetEndChunksTransform();
    }

    /// <summary>
    /// Creates a node graph where end nodes will be individual chunks of land
    /// </summary>
    private static ChunkGraphNode CreatePartitionGraph(Vector2Int graphSize, System.Func<int> maxChunkSizeFunc)
    {
        ChunkGraphNode rootGraphNode = new(Vector2Int.zero, graphSize - Vector2Int.one);

        //perform chunk partition
        RecursivePartitionChunk(ref rootGraphNode, maxChunkSizeFunc);

        return rootGraphNode;
    }


    /// <summary>
    /// Recursively Partition Chunks into smaller sub-chunks as childs
    /// </summary>
    private static void RecursivePartitionChunk(ref ChunkGraphNode chunkNode, System.Func<int> maxChunkSizeFunc)
    {
        //Return if chunk is small enough or cannot be divided furthur
        if(chunkNode.chunkTransform.ChunkCellCount < maxChunkSizeFunc.Invoke() || (chunkNode.chunkTransform.ChunkWidth < 3 && chunkNode.chunkTransform.ChunkHeight < 3))
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
        ref ChunkTransform chunkTransform = ref chunkToPart.chunkTransform;

        //Find the direction to part the chunk
        if(chunkTransform.ChunkWidth < 3)
        {
            partDirection = lineDirection.alongX;
        }
        else if(chunkTransform.ChunkHeight < 3)
        {
            partDirection = lineDirection.alongY;
        }
        else
        {
            //select direction randomly weighted in advantage to split the 'longer' side
            partDirection = DoStatic.RandomBool(chunkTransform.ChunkHeight/(float)(chunkTransform.ChunkHeight + chunkTransform.ChunkWidth)) ? lineDirection.alongX : lineDirection.alongY;
        }

        //Perform the partition, use GaussianRandom so the chunk is more likely to be split near the center
        const float centeringPower = 0.2f;
        int splitPosition;
        switch(partDirection)
        {
            case lineDirection.alongX:
            splitPosition = Mathf.Clamp(Mathf.RoundToInt(AdvanceRandom.GaussianRandom( (chunkTransform.bottomRight.y + chunkTransform.upperLeft.y)/2, Mathf.Pow(chunkTransform.ChunkHeight, centeringPower))), chunkTransform.upperLeft.y + 1, chunkTransform.bottomRight.y - 1);
            chunkToPart.leftChild = new(new(chunkTransform.upperLeft.x, chunkTransform.upperLeft.y), new(chunkTransform.bottomRight.x, splitPosition - 1));
            chunkToPart.rightChild = new(new(chunkTransform.upperLeft.x, splitPosition + 1), new(chunkTransform.bottomRight.x, chunkTransform.bottomRight.y));
            //Debug.Log("Chunk: UL:" + chunkToPart.upperLeft + ",BR:" + chunkToPart.bottomRight + ", SplitYPos:" + splitPosition);
            break;

            case lineDirection.alongY:
            splitPosition = Mathf.Clamp(Mathf.RoundToInt(AdvanceRandom.GaussianRandom( (chunkTransform.bottomRight.x + chunkTransform.upperLeft.x)/2, Mathf.Pow(chunkTransform.ChunkWidth, centeringPower))), chunkTransform.upperLeft.x + 1, chunkTransform.bottomRight.x - 1);
            chunkToPart.leftChild = new(new(chunkTransform.upperLeft.x, chunkTransform.upperLeft.y), new(splitPosition - 1, chunkTransform.bottomRight.y));
            chunkToPart.rightChild = new(new(splitPosition + 1, chunkTransform.upperLeft.y), new(chunkTransform.bottomRight.x, chunkTransform.bottomRight.y));
            //Debug.Log("Chunk: UL:" + chunkToPart.upperLeft + ",BR:" + chunkToPart.bottomRight + ", SplitXPos:" + splitPosition);
            break;
        }
    }
}
