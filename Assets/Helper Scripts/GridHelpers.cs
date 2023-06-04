using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridHelpers
{
    public static Vector3 GridToWorldPos(int xIndex, int yIndex, int sizeX, int sizeY)
    {
        float xPos = xIndex;
        float yPos = sizeY - yIndex;
        return new Vector3(xPos, yPos, 0f);
    }
}
