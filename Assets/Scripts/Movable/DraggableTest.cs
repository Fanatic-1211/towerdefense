using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.Input
{
    public class DraggableTest : MonoBehaviour, IMouse
    {
        Camera currentCamera;
        private void Awake()
        {
            currentCamera = Camera.main;
        }
        public void OnInputMouseDown(InputAction.CallbackContext context)
        {
            
        }

        public void OnInputMouseDragScreenCoordinates(Vector3 context)
        {
            this.transform.position += context;
        }

        public void OnInputMouseUp(InputAction.CallbackContext context)
        {
          
        }

    } }
