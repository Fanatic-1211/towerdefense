using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class BuyableItemCard : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI titleText;
    public event Action OnItemPressed;
    bool active = true;
   
    public void SetUp(Sprite sprite, int cost,string title)
    {
        image.sprite = sprite;
        costText.text = cost.ToString();
        titleText.text = title;
        active = true;
    }
    public void SetUp(IBuyable buyable)
    {
        image.sprite = buyable.GetItemSprite();
        costText.text = buyable.GetCost().ToString();
        titleText.text = buyable.GetTowerName();
        active = true;
    }
    public void Active(bool active)
    {
        this.active = active;
        image.color = active ? Color.white : Color.gray;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (active)
            OnItemPressed?.Invoke();
    }
}
