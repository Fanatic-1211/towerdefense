using Game.GameSystem.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.CameraControll
{
    public class CameraAddon : MonoBehaviour ,IMouse, IOnEdgeOfScreenDragging
    {
        private Camera thisCamera;
        Mouse mouse = Mouse.current;
        private float scrollSpeed=50f;
        private bool onDraggingInAction = false;
        public void CursorOnEdgeOfScreen(Vector2 edgeVector)
        {
            if(!onDraggingInAction)
            thisCamera.transform.position += new Vector3(edgeVector.x, 0, edgeVector.y) * Time.deltaTime* scrollSpeed;
        }

    

        public Vector3 FromCameraToScreenPosition(Vector2 delta)
        {
            Vector2 totalDrag = delta;
            totalDrag += new Vector2(thisCamera.pixelWidth, thisCamera.pixelHeight) / 2;
            Vector3 toPoint = thisCamera.ScreenToWorldPoint(totalDrag);
            
            return toPoint;
        }
        public void OnInputMouseDown(InputAction.CallbackContext context)
        {
            onDraggingInAction = true;
        }

        public void OnInputMouseDrag(Vector2 delta)
        {
            Vector3 targetPosition = FromCameraToScreenPosition(-delta);
            targetPosition.y = thisCamera.transform.position.y;
            thisCamera.transform.position= targetPosition;
        }

        public void OnInputMouseUp(InputAction.CallbackContext context)
        {
            onDraggingInAction = false;
        }

        private void Awake()
        {
            thisCamera = GetComponent<Camera>();
        }
    }
}