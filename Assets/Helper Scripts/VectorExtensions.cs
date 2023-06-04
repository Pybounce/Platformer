using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Mul(this Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
    public static Vector3 Abs(this Vector3 v1)
    {
        return new Vector3(Mathf.Abs(v1.x), Mathf.Abs(v1.y), Mathf.Abs(v1.z));
    }
}
