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
        public void OnMouseDown(InputAction.CallbackContext context)
        {
            
        }

        public void OnMouseDrag(Vector2 context)
        {

            Vector2 totalDrag = context;
            totalDrag += new Vector2(currentCamera.pixelWidth, currentCamera.pixelHeight) / 2;
            Vector3 toPoint = currentCamera.ScreenToWorldPoint(totalDrag);
            
            this.transform.position += toPoint;
        }

        public void OnMouseUp(InputAction.CallbackContext context)
        {
          
        }

    } }
