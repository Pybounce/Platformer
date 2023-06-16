using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridHelpers
{
    public static Vector3 GridToWorldPos(int xIndex, int yIndex, int sizeX, int sizeY, Vector3 gridPosition = default, int cellSize = 1)
    {
        float xPos = xIndex * cellSize;
        float yPos = (sizeY - yIndex) * cellSize;
        return new Vector3(xPos, yPos, 0f) + gridPosition;
    }
    public static Vector3 GridToWorldPos(Vector2Int index, Vector2Int size, Vector3 gridPosition = default, int cellSize = 1)
    {
        float xPos = index.x * cellSize;
        float yPos = (size.y - index.y) * cellSize;
        return new Vector3(xPos, yPos, 0f) + gridPosition;
    }
}
//TODO => Make an actual Grid data structure and this can be GridExtensions