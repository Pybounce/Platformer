using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PropData
{
    public int Id;
    public PropDirection Rotation;  //Change to direction
}
[System.Serializable]
public enum PropDirection
{
    Up=0,
    Right=90,
    Down=180,
    Left=270
}