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
    private void Construct(IBillingSystem bank, IMarket market, DraggableManager draggableManager)
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
        draggableManager.StartDragging(towerData.TowerPrefab.gameObject, (placeable) => CreateTower(towerData, placeable));
    }
    public void CreateTower(TowerData towerData, ITowerPlaceable placeable)
    {
        if (placeable != null)
            if (placeable.IsCellAvalable()&& bank.CanBuy(towerData.GetCost()))
            {
                bank.Withdraw(towerData.GetCost());
                activeTowers.Add(placeable.PlaceTower(towerData.TowerPrefab));
            }
    }
}
