using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SMatrix<T> where T: class, new()
{
    public List<Array> arrays = new List<Array>();
    public T this[int x, int y] => arrays[x][y];
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
public class Extensions : MonoBehaviour
{
    public static void DisposeObject<T>(List<T> obj) where T : MonoBehaviour
    {
        for (int i = obj.Count - 1; i >= 0; i--)
        {
            Destroy(obj[i].gameObject);
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
