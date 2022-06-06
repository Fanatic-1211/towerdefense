using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
namespace Game.Environment.Map
{
    public class TilePicker : MonoBehaviour
    {

        public event Action<ISelectable> OnSelectablePicked;
        public event Action<ISelectable> OnSelectablePickedWithShift;
        /// <summary>
        /// Cast a ray to test if Input.mousePosition is over any UI object in EventSystem.current. This is a replacement
        /// for IsPointerOverGameObject() which does not work on Android in 4.6.0f3
        /// </summary>
        private bool IsPointerOverUIObject()
        {
            // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
            // the ray cast appears to require only eventData.position.
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        private void Update()
        {
            if (IsPointerOverUIObject()) return;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray.origin, (ray.direction - Camera.main.transform.position) * 10, out hitInfo);
                ISelectable tile = hitInfo.collider.gameObject.GetComponent<ISelectable>();
                if (tile != null)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        OnSelectablePickedWithShift?.Invoke(tile);
                    }
                    else
                    {
                        OnSelectablePicked?.Invoke(tile);
                    }
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
