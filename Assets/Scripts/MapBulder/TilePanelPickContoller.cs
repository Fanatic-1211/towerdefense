using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Environment.Map
{
    public class TilePanelPickContoller : MonoBehaviour
    {
        [SerializeField] TilePanelItem tilePanelItemPrefab;
        [SerializeField] Transform targetParent;
        List<TilePanelItem> tilePanelItems = new List<TilePanelItem>();
        [Zenject.Inject] TileMeshLibrary meshLibrary;
        public event Action<string> OnTilePicked;
        private string currentPickedTileName;
        public string CurrentPickedTileName  => currentPickedTileName;

        private void Start()
        {
            Extensions.DisposeObject(tilePanelItems);
            tilePanelItems.Clear();
            foreach (var item in meshLibrary.GetAllTileMeshes())
            {
                TilePanelItem tileCard = Instantiate(tilePanelItemPrefab, targetParent);
                tileCard.SetUp(item.meshName, item.meshPreview);
                tileCard.OnItemPicked +=() => OnItemPicked(item.meshName);
                tilePanelItems.Add(tileCard);
            }
        }
        private void OnItemPicked(string name)
        {
            OnTilePicked?.Invoke(name);
        }

    }
}