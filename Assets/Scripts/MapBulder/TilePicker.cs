using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

namespace Game.Environment.Map
{
    public class TilePicker : MonoBehaviour
    {
        private PlayerInputControlls playerInputAction;
        public event Action<EditableTile> OnSelectablePicked;
        public event Action<EditableTile> OnSelectableMultiplePicked;

        private void Awake()
        {
            playerInputAction = new PlayerInputControlls();
            playerInputAction.Player.Enable();
            playerInputAction.Player.Pick.performed += Pick_performed;
            playerInputAction.Player.PickMultiple.performed += PickMultiple_performed;
        }

        private void PickMultiple_performed(InputAction.CallbackContext obj)
        {
            if (IsPointerOverUIObject()) return;
           
            EditableTile pickedTile = FindTileByRaycast();
            if (pickedTile != null)
            {
                OnSelectableMultiplePicked?.Invoke(pickedTile);
            }
        }

        private void Pick_performed(InputAction.CallbackContext obj)
        {
            if (IsPointerOverUIObject()) return;
            EditableTile pickedTile = FindTileByRaycast();
            if (pickedTile != null)
            {
                OnSelectablePicked?.Invoke(pickedTile);
            }
        }
        private EditableTile FindTileByRaycast()
        {
            RaycastHit hitInfo;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray.origin, (ray.direction - Camera.main.transform.position) * 10, out hitInfo);
            return hitInfo.collider.gameObject.GetComponent<EditableTile>();

        }

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
      
    }
}
