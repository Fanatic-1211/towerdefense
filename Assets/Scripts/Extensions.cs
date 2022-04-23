using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extensions : MonoBehaviour
{
    public static void DisposeObject<T>(List<T> obj) where T : MonoBehaviour
    {
        for (int i = obj.Count-1; i >= 0 ; i--)
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
