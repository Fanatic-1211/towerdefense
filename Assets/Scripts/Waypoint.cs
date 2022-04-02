using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    Tower occupation;
    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable => occupation==null&& isPlaceable;
   
    private void OnMouseDown()
    {
        if (isPlaceable&& occupation==null)
        {
            occupation = towerPrefab.CreateTower(towerPrefab,transform.position);
        }
    }

}
