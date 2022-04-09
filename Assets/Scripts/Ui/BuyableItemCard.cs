using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class BuyableItemCard : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI titleText;
    public event Action OnItemPressed;
    bool active = true;
    private void Awake()
    {
        button.onClick.AddListener(OnButtonPressed);
    }
    public void SetUp(Sprite sprite, int cost,string title)
    {
        image.sprite = sprite;
        costText.text = cost.ToString();
        titleText.text = title;
        active = true;
    }

    public void Active(bool active)
    {
        this.active = active;
        button.interactable = active;
    }
    private void OnButtonPressed()
    {
        if (active)
            OnItemPressed?.Invoke();
    }
}
