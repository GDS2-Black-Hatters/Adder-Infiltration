using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTransform
{
    [System.Flags]
    public enum ChunkCellPosition
    {
        center = 1,
        edge = 2,
        corner = 4
    }

    public ChunkTransform(Vector2Int upperLeft, Vector2Int bottomRight)
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

    public int ChunkCellCount
    {
        get { return ChunkWidth * ChunkHeight; }
    }

    public Vector2 ChunkCenter
    {
        get { return new Vector2((float)(upperLeft.x + bottomRight.x)/2, (float)(upperLeft.y + bottomRight.y)/2); }
    }

    public bool IsWithinChunk(Vector2Int position)
    {
        return !(position.x < upperLeft.x || position.x > bottomRight.x || position.y < upperLeft.y || position.y > bottomRight.y) ;
    }

    public ChunkCellPosition CellPosition(Vector2Int position)
    {
        bool onXEdge = false;
        bool onYEdge = false;

        if(position.x == upperLeft.x || position.x == bottomRight.x)
            onXEdge = true;

        if(position.y == upperLeft.y || position.y == bottomRight.y)
            onYEdge = true;


        if(onXEdge && onYEdge)
            return ChunkCellPosition.corner;

        if(onXEdge || onYEdge)
            return ChunkCellPosition.edge;

        return ChunkCellPosition.center;
    }
}
