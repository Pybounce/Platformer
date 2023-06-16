using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct StageData
{
    public readonly int Id;
    public Vector2Int Size;
    public List<PropData> PropData;
    public Vector2Int PlayerStartIndex;
    public StageData(int id, Vector2Int size, List<PropData> propData, Vector2Int playerStartIndex)
    {
        Id = id;
        Size = size;
        PropData = propData;
        PlayerStartIndex = playerStartIndex;
    }

    public static StageData DefaultStage => new StageData(-1, Vector2Int.zero, null, Vector2Int.zero);

}