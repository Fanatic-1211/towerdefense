using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Environment.Map
{
    public class TilePicker : MonoBehaviour
    {
        private ISelectable currentTargetable;
        private GameObject pickedGameObject;
        public EditableTile PickedTile => pickedGameObject.GetComponent<EditableTile>();
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, (ray.direction - Camera.main.transform.position) * 10, Color.red, 5f);
                Physics.Raycast(ray.origin, (ray.direction - Camera.main.transform.position) * 10, out hitInfo);
                Debug.Log(ray.direction + " " + ray.origin);
                ISelectable tile = hitInfo.collider.gameObject.GetComponent<ISelectable>();
                if (tile != null && tile != currentTargetable)
                {
                    Debug.Log($"Was hit target {hitInfo.collider.gameObject.name}");
                    currentTargetable?.DeselectTarget();
                    tile.SelectTarget();
                    currentTargetable = tile;
                    pickedGameObject = hitInfo.collider.gameObject;
                }
            }

            /*
            Debug.DrawRay(transform.position, Vector3.down * 100f, Color.red);
            ITowerPlaceable newTile = null;
            if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1000f, draggableIgnore))
                newTile = hit.collider.gameObject.GetComponent<ITowerPlaceable>();
            if (newTile != currentTile)
            {
                if (currentTile != null) LeaveTile(currentTile);
                if (newTile != null) EnterTile(newTile);
                currentTile = newTile;
            }
            if (Input.GetMouseButtonUp(0)) OnDragEnded?.Invoke(currentTile);
            transform.position = FindPanelByRaycast();
            */
        }
    }
}
