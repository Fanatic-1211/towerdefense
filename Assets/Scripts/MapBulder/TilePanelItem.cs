using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Environment.Map
{
    public class TilePanelItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemText;
        [SerializeField] Image itemImage;
        [SerializeField] Button button;
        public Action OnItemPicked;

        private void Awake()
        {
            button.onClick.AddListener(OnButtopClicked);
        }
        private void OnButtopClicked()
        {
            OnItemPicked?.Invoke();
        }
        public void SetUp(string text,Sprite sprite)
        {
            itemImage.sprite = sprite;
            itemText.text = text;
        }
        
    }
}