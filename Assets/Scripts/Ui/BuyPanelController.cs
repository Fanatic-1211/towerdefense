using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuyPanelController : MonoBehaviour, IMarket
{
    [Inject] IBillingSystem billingSystem;
    [SerializeField] Transform cardParent;
    [SerializeField] BuyableItemCard cardPrefab;
    List<BuyableItemCard> currentBuyables = new List<BuyableItemCard>();

    public void SetObjects<T>(List<T> buyables, Action<T> callBack) where T : IBuyable
    {
        Extensions.DisposeObject(currentBuyables);
        foreach (var item in buyables)
        {
            BuyableItemCard card = InstantiateCard(item);
            card.OnItemPressed +=()=> OnCardPicked(item,()=> callBack(item));
            currentBuyables.Add(card);
        }
    } 

    private BuyableItemCard InstantiateCard(IBuyable buyable)
    {
        BuyableItemCard cart = Instantiate(cardPrefab, cardParent);
        cart.SetUp(buyable);
        return cart;
    }
    private void OnCardPicked(IBuyable card,Action callback)
    {
        if (billingSystem.GetCurrentBalance() >= card.GetCost())
            callback?.Invoke();
    }
}
