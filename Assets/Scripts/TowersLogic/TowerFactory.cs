using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
public class TowerFactory : MonoBehaviour
{
    IBillingSystem bank;
    IMarket market;
    DraggableManager draggableManager;
    [SerializeField] List<TowerData> towersData;
    List<Tower> activeTowers = new List<Tower>();

    [Inject]
    private void Construct(IBillingSystem bank, IMarket market,DraggableManager draggableManager)
    {
        this.bank = bank;
        this.market = market;
        this.draggableManager = draggableManager;
    }
    private void Start()
    {
         market.SetObjects(towersData, CreateTower);
    }
    private void CreateTower(TowerData towerData)
    {
        Debug.Log($"Picked tower with name {towerData.name}");
        draggableManager.StartDragging(towerData.TowerPrefab.gameObject);
    }
    public bool CreateTower(Vector3 position)
    {
        return false;
        /* if (towerPrefab.TowerCost > bank.GetCurrentBalance())
             return false;
         activeTowers.Add(Instantiate(towerPrefab, position, Quaternion.identity, this.transform));
         bank.Withdraw(towerPrefab.TowerCost);
         return true;*/
    }
}
