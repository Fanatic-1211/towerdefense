using Game.GameSystem.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.CameraControll
{
    public class CameraController : MonoBehaviour ,IMouse
    {
        private Camera thisCamera;
        Mouse mouse = Mouse.current;
      //  Vector2 totalDrag = Vector2.zero;
        public void OnMouseDown(InputAction.CallbackContext context)
        {
          //  throw new System.NotImplementedException();
        }

        public void OnMouseDrag(InputAction.CallbackContext context)
        {
            Vector2 totalDrag =-context.ReadValue<Vector2>();
            totalDrag += new Vector2(thisCamera.pixelWidth, thisCamera.pixelHeight) / 2;
            Vector3 toPoint = thisCamera.ScreenToWorldPoint(totalDrag);
           
            Debug.Log($"Delta was {totalDrag} to point was  {toPoint}");
            thisCamera.transform.position = toPoint;
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