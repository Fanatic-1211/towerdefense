using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Waypoint : MonoBehaviour
{
    [Inject]
    TowerFactory TowerFactory;
    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable => isPlaceable;
   
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            isPlaceable = TowerFactory.CreateTower(transform.position);
        }
    }

}
