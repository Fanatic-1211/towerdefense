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
}
