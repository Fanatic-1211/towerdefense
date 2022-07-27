using Game.GameSystem.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.CameraControll
{
    public class CameraAddon : MonoBehaviour ,IMouse
    {
        private Camera thisCamera;
        Mouse mouse = Mouse.current;
       
        public Vector3 FromCameraToScreenPosition(Vector2 delta)
        {
            Vector2 totalDrag = delta;
            totalDrag += new Vector2(thisCamera.pixelWidth, thisCamera.pixelHeight) / 2;
            Vector3 toPoint = thisCamera.ScreenToWorldPoint(totalDrag);
            
            return toPoint;
        }
        public void OnMouseDown(InputAction.CallbackContext context)
        {
          //  throw new System.NotImplementedException();
        }

        public void OnMouseDrag(Vector2 delta)
        {
            
            thisCamera.transform.position = FromCameraToScreenPosition(-delta);
        }

        public void OnMouseUp(InputAction.CallbackContext context)
        {
           // throw new System.NotImplementedException();
        }

        private void Awake()
        {
            thisCamera = GetComponent<Camera>();
        }
    }
}