using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Game.Environment.Map
{
    public class TilePickedManager : MonoBehaviour
    {
        [SerializeField] TilePicker tilePicker;
        [SerializeField] TilePanelPickContoller panelPickContoller;
        [Inject] MapCreator mapCreator;
        private void Awake()
        {
            panelPickContoller.OnTilePicked += SetTileData;
            panelPickContoller.OnRotation += PanelPickContoller_OnRotation;
        }

        private void PanelPickContoller_OnRotation()
        {
            mapCreator.RotateTile(tilePicker.PickedTile);
        }

        private void SetTileData(string meshName)
        {

            mapCreator.UpdateTileMesh(meshName, tilePicker.PickedTile);
        }
       
    }
}