using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Game.Environment.Map
{
    public class TilePickedManager : MonoBehaviour
    {
        [SerializeField] TilePicker tilePicker;
        [SerializeField] TilePanelMeshView panelMeshView;
        [Inject] MapCreator mapCreator;
        private ISelectable currentTargetable;
        private GameObject pickedGameObject;
        public EditableTile PickedTile => pickedGameObject.GetComponent<EditableTile>();
        private void Awake()
        {
            panelMeshView.OnTilePicked += SetTileData;
            panelMeshView.OnRotation += PanelPickContoller_OnRotation;
            tilePicker.OnSelectablePicked += TilePicker_OnSelectablePicked;
            tilePicker.OnSelectablePickedWithShift += TilePicker_OnSelectablePickedWithShift;
        }

        private void TilePicker_OnSelectablePickedWithShift(ISelectable obj)
        {
            throw new System.NotImplementedException();
        }

        private void TilePicker_OnSelectablePicked(ISelectable obj)
        {
            throw new System.NotImplementedException();
        }

        private void PanelPickContoller_OnRotation()
        {
            mapCreator.RotateTile(tilePicker.PickedTile);
        }

        private void SetTileData(string meshName)
        {
            mapCreator.UpdateTileMesh(meshName, tilePicker.PickedTile);
        }
       
        public void HighLightTile()
        {
            if (tile != null && tile != currentTargetable)
            {
                Debug.Log($"Was hit target {hitInfo.collider.gameObject.name}");
                if (currentTargetable != null) currentTargetable.DeselectTarget();
                tile.SelectTarget();
                currentTargetable = tile;
                pickedGameObject = hitInfo.collider.gameObject;
            }
        }
    }
}