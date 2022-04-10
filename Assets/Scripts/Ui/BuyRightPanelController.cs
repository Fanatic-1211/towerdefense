using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRightPanelController : MonoBehaviour
{
    [SerializeField] Transform cardParent;
    [SerializeField] BuyableItemCard cardPrefab;
    List<BuyableItemCard> currentBuyables = new List<BuyableItemCard>();
  
    public void SetPanelData(List<IBuyable> buyables)
    {
        Extensions.DisposeObject(currentBuyables);
        foreach (var item in buyables)
        {
            currentBuyables.Add(InstantiateCard(item));
        }
    }
    private BuyableItemCard InstantiateCard(IBuyable buyable)
    {
        BuyableItemCard cart = Instantiate(cardPrefab, cardParent);
        cart.SetUp(buyable);
        
        return cart;
    }
    private void OnCardPicked()
    {

    }
}
