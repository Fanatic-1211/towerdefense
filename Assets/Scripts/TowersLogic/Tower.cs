using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour
{
    [Inject]
    IBillingSystem bank;
    [SerializeField] TargetLocator targetLocator;
    [SerializeField] SimpleWeapon weaponSystem;
    private void Awake()
    {
        targetLocator.OnNewTargetArrive += TargetLocator_OnNewTargetArrive;
        targetLocator.OnNoTargets += TargetLocator_OnNoTargets;
    }

    private void TargetLocator_OnNoTargets()
    {
        weaponSystem.ClearTargets();
    }

    private void TargetLocator_OnNewTargetArrive(ITargetable obj)
    {
        weaponSystem.SetNewTarget(obj);
    }
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
