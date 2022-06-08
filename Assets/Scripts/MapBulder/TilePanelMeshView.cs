using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Environment.Map
{
    public class TilePanelMeshView : MonoBehaviour
    {
        [SerializeField] TilePanelItem tilePanelItemPrefab;
        [SerializeField] Transform targetParent;
        [SerializeField] Button button;
        List<TilePanelItem> tilePanelItems = new List<TilePanelItem>();
        [Zenject.Inject] TileMeshLibrary meshLibrary;
        public event Action<string> OnTilePicked;
        public event Action OnRotation;
        private string currentPickedTileName;
        public string CurrentPickedTileName  => currentPickedTileName;
        private void Awake()
        {
            button.onClick.AddListener(RotateTile);
        }
        private void Start()
        {
            Extensions.DisposeObject(tilePanelItems);
            tilePanelItems.Clear();
            foreach (var item in meshLibrary.GetAllTileMeshes())
            {
                TilePanelItem tileCard = Instantiate(tilePanelItemPrefab, targetParent);
                tileCard.SetUp(item.meshName, item.meshPreview);
                tileCard.OnItemPicked +=() => OnItemPicked(item.meshName);
                tileCard.gameObject.SetActive(true);
                tilePanelItems.Add(tileCard);
            }
        }
        private void RotateTile()
        {
            OnRotation?.Invoke();
        }
        private void OnItemPicked(string name)
        {
            OnTilePicked?.Invoke(name);
        }

    }
}