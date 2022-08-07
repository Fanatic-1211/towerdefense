using Game.GameSystem.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.CameraControll
{
    public class CameraAddon : MonoBehaviour, IMouse, IOnEdgeOfScreenDragging, ICameraMoved, IScreenToWorldConverter
    {
        private Camera thisCamera;
        Mouse mouse = Mouse.current;
        private float scrollSpeed = 50f;
        private bool onDraggingInAction = false;
        public event Action<Vector3> OnCameraMovedDelta;

        public void CursorOnEdgeOfScreen(Vector2 edgeVector)
        {
            if (onDraggingInAction) return;
            Vector3 moveDelta = new Vector3(edgeVector.x, 0, edgeVector.y) * Time.deltaTime * scrollSpeed;
            OnCameraMovedDelta?.Invoke(moveDelta);
            thisCamera.transform.position += moveDelta;
        }



        public void OnInputMouseDown(InputAction.CallbackContext context)
        {
            onDraggingInAction = true;
        }

        public void OnInputMouseDragScreenCoordinates(Vector3 delta)
        {
            
            thisCamera.transform.position += new Vector3(-delta.x,0, -delta.z);
        }

        public void OnInputMouseUp(InputAction.CallbackContext context)
        {
            onDraggingInAction = false;
        }

        private void Awake()
        {
            thisCamera = GetComponent<Camera>();
        }

        public void GetOnCameraWasMovedDeltaActionSubscribe(Action<Vector3> deltaEvent)
        {
            OnCameraMovedDelta += deltaEvent;

        }

        public void GetOnCameraWasMovedDeltaActionUnsubscribe(Action<Vector3> deltaEvent)
        {
            OnCameraMovedDelta -= deltaEvent;
        }

        public Vector3 FromScreenToWorld(Vector2 screenCoord)
        {
            return thisCamera.ScreenToWorldPoint(screenCoord);
        }

        public Vector3 FromScreenDeltaToWorldDelta(Vector2 delta)
        {
            Vector2 totalDrag = delta;
            totalDrag += new Vector2(thisCamera.pixelWidth, thisCamera.pixelHeight) / 2;
            Vector3 toPoint = thisCamera.ScreenToWorldPoint(totalDrag);
            toPoint -= thisCamera.transform.position;
            return toPoint;
        }
    }
}