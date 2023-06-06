using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public readonly int Id;
    public PropData[] PropData;
    public int PropDataWidth;
    public Vector3 PlayerStartPosition;
    public StageData(int id, PropData[] tiles, int propDataWidth, Vector3 playerStartPosition)
    {
        Id = id;
        PropData = tiles;
        PropDataWidth = propDataWidth;
        PlayerStartPosition = playerStartPosition;
    }

}
    /*
    public StageData(StageJsonData jsonData, int id)
    {
        int width = jsonData.PropDataWidth;
        int height = jsonData.PropData.Length / width;

        PropData[,] propData = new PropData[height, width];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = (y * width) + x;
                propData[y, x] = jsonData.PropData[index];
            }
        }
        Id = id;
        PropData = propData;
        PlayerStartPosition = jsonData.PlayerStartPosition;
    }
}

[System.Serializable]
public struct StageJsonData
{
    public PropData[] PropData;
    public Vector3 PlayerStartPosition;
    public int PropDataWidth;
    public StageJsonData(PropData [] tiles, Vector3 playerStartPosition, int propDataWidth)
    {
        PropData = tiles;
        PlayerStartPosition = playerStartPosition;
        PropDataWidth = propDataWidth;
    }
}*/