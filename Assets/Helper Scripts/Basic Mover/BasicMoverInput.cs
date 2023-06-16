using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data used in the BasicMover script
/// </summary>
[System.Serializable]
public struct BasicMoverInput
{
    public bool Enabled;
    public Vector3 Offset;
    public float Speed;
    public float StartLerp;
}