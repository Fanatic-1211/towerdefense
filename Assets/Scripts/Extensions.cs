using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SMatrix<T> where T : class, new()
{
    public Vector2Int MatrixSize => new Vector2Int(arrays.Count, arrays.Count > 0 ? arrays[0].cells.Count : 0);

    public List<Array> arrays = new List<Array>();
    public T this[int x, int y] => arrays[x][y];
    public T this[Vector2Int vector2Int] => arrays[vector2Int.x][vector2Int.y];
    public SMatrix(Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            arrays.Add(new Array());
            for (int y = 0; y < size.y; y++)
            {
                arrays[x].cells.Add(new T());
            }
        }
    }
    [System.Serializable]
    public class Array
    {
        public List<T> cells = new List<T>();
        public T this[int index] => cells[index];
    }
}
public static class Extensions
{
    public static (int, int) CoordinatesOf<T>(this T[,] matrix, T value)
    {
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y].Equals(value))
                    return (x, y);
            }
        }

        return (-1, -1);
    }
    public static Vector2Int CoordinatesOfVector2<T>(this T[,] matrix, T value)
    {
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y].Equals(value))
                    return new Vector2Int(x, y);
            }
        }

        return new Vector2Int(-1, -1);
    }
    public static void DisposeMatrix<T>(T[,] matrix) where T : MonoBehaviour
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                MonoBehaviour.Destroy(matrix[x, y].gameObject);
            }
        }
    }
    public static void DisposeObject<T>(List<T> obj) where T : MonoBehaviour
    {
        for (int i = obj.Count - 1; i >= 0; i--)
        {
            MonoBehaviour.Destroy(obj[i].gameObject);
        }
        obj.Clear();
    }
    public static T GetClosestOnbject<T>(T target, List<T> totalList) where T : MonoBehaviour
    {
        T tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = target.transform.position;
        foreach (T t in totalList)
        {
            if (t == target) continue;
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
