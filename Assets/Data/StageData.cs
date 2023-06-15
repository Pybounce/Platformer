using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public static StageData DefaultStage => new StageData(-1, null, 0, Vector3.zero);
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