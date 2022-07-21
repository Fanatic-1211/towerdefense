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

        public void OnMouseDown(InputAction.CallbackContext context)
        {
          //  throw new System.NotImplementedException();
        }

        public void OnMouseDrag(InputAction.CallbackContext context)
        {
            Vector2 delta = -context.ReadValue<Vector2>();
            thisCamera.transform.position += new  Vector3(delta.x,0,delta.y);
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