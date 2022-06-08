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
        private List<EditableTile> currentSelectedTargets = new List<EditableTile>();
        private EditableTile originSelectable;
        private GameObject pickedGameObject;
        private void Awake()
        {
            panelMeshView.OnTilePicked += SetTileData;
            panelMeshView.OnRotation += PanelPickContoller_OnRotation;
            tilePicker.OnSelectablePicked += TilePicker_OnSelectablePicked;
            tilePicker.OnSelectablePickedWithShift += TilePicker_OnSelectablePickedWithShift;
        }

        private void TilePicker_OnSelectablePickedWithShift(EditableTile obj)
        {
          
            if (originSelectable == null)
            {
                TilePicker_OnSelectablePicked(obj);
            }
            else
            {
                DeselectTargets(currentSelectedTargets);
                currentSelectedTargets.AddRange(mapCreator.GetTileListByTileCorners(originSelectable, obj));
                SelectTargets(currentSelectedTargets);
            }

        }

        private void TilePicker_OnSelectablePicked(EditableTile obj)
        {
            originSelectable = obj;
            DeselectTargets(currentSelectedTargets);
            currentSelectedTargets.Add(originSelectable);
            SelectTargets(currentSelectedTargets);
        }

        private void PanelPickContoller_OnRotation()
        {
            currentSelectedTargets.ForEach(t => mapCreator.RotateTile(t));
        }

        private void SetTileData(string meshName)
        {
            currentSelectedTargets.ForEach(t => mapCreator.UpdateTileMesh(meshName, t));
           
        }

        private void SelectTargets(List<EditableTile> targetList)
        {
            foreach (var item in targetList)
            {
                item.SelectTarget();
            }
        }
        private void DeselectTargets(List<EditableTile> targetList)
        {
            foreach (var item in targetList)
            {
                item.DeselectTarget();
            }
            targetList.Clear();
        }
    }
}