using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour
{
    [Inject]
    IBillingSystem bank;

    /*public Tower CreateTower(Tower tower, Vector3 position)
    {

        if (bank == null)
        {
            return null;
        }
        if (bank.GetCurrentBalance() >= cost)
        {
            bank.Withdraw(cost);
            return DiContainer.InstantiatePrefab(tower, position, Quaternion.identity,this.transform);
        }
        return null;
    }*/
}