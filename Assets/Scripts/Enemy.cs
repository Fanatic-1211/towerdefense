using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward=25;
    [SerializeField] int goldPenalty = 25;
    [Zenject.Inject]
    IBillingSystem bank;
  
   
    public void TargetDied()
    {
        bank.Deposit(goldReward);
    }
    public void TargetReachedEnd()
    {
        bank.Withdraw(goldPenalty);
    }

}
