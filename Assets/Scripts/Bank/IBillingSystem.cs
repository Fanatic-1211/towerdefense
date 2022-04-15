using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBillingSystem 
{
    public int GetCurrentBalance();
    public bool Deposit(int amount);
    public bool Withdraw(int amount);
    public bool CanBuy(int cost);
}
