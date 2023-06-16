using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PropData
{
    public int Id;
    public Vector2Int GridIndex;
    public PropDirection Direction;
    public PropMoverData MoverData;
    public PropRotatorData RotatorData;
}
