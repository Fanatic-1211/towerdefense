using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class TowerFactory : MonoBehaviour
{
    IBillingSystem bank;
    [SerializeField] Tower towerPrefab;
    List<Tower> activeTowers = new List<Tower>();

    [Inject]
    private void Construct(IBillingSystem bank)
    {
        this.bank = bank;
    }
    public bool CreateTower(Vector3 position)
    {
        if (towerPrefab.TowerCost > bank.GetCurrentBalance())
            return false;
        activeTowers.Add(Instantiate(towerPrefab, position, Quaternion.identity, this.transform));
        bank.Withdraw(towerPrefab.TowerCost);
        return true;
    }
}
