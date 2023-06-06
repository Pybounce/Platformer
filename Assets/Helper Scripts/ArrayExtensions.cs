using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ArrayExtensions
{
    public static Vector2Int Get2DDimentions<T>(this T[] arr, int width)
    {
        int height = arr.Length / width;
        return new Vector2Int(width, height);
    }
    public static int FlattenIndex(int x, int y, int width)
    {
        return x + (y * width);
    }
}
