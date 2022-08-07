using Game.GameSystem.CameraControll;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.InputSystem.InputAction;

namespace Game.GameSystem.Input
{
    public class GameInputManager : MonoBehaviour
    {
        private MainInputAction inputAction;
        public event Action<Vector2> OnPrimaryTouchPressed;
        public event Action<Vector2> OnHoldTouchPresed;
        private DoubleClickAddon doubleClickAddon;
        private event Action<Vector3> OnDragInPerformingWorldCoordinates;
        private event Action<Vector2> OnDragInActionWorldCoordinates;
        private bool applicationWasUnfocusedOrStarted = true;
        private bool isApplicationInFocus = false;
        private bool onEdgeOfScreen = false;
        private float distanceFromEdge = 5f;
        [SerializeField] CameraAddon cameraController;
        IMouse defautDraggable => cameraController;
        IMouse currentDragger;
        IOnEdgeOfScreenDragging onEdgeOfScreenListerner => cameraController;
        ICameraMoved cameraMoved => cameraController;
        IScreenToWorldConverter screenToWorldConverter => cameraController;
        Vector2 pastPointerValue = Vector2.zero;
        private void Awake()
        {
            doubleClickAddon = GetComponent<DoubleClickAddon>();
            inputAction = new MainInputAction();
            inputAction.TouchInput.Enable();
            inputAction.TouchInput.PrimaryContact.performed += OnClickStarted;
            inputAction.TouchInput.PrimaryContact.canceled += OnClickCanceled;
            inputAction.TouchInput.LongPress.performed += OnHoldPress;
            //  inputAction.TouchInput.TouchDeltaMove.performed += TouchDeltaMove_performed;
            pastPointerValue = inputAction.TouchInput.PrimaryPosition.ReadValue<Vector2>();
            cameraMoved.GetOnCameraWasMovedDeltaActionSubscribe(OnCameraMoved);
            applicationWasUnfocusedOrStarted = true;
        }
        private void OnDestroy()
        {
            cameraMoved.GetOnCameraWasMovedDeltaActionUnsubscribe(OnCameraMoved);
        }
        private void OnCameraMoved(Vector3 delta)
        {
            OnDragInPerformingWorldCoordinates?.Invoke(delta);
        }
        private void OnApplicationFocus(bool focus)
        {
            isApplicationInFocus = focus;
            Debug.Log($"Is focused {focus}");
            if (focus) applicationWasUnfocusedOrStarted = true;
        }

        private void TouchDeltaMove_performed(CallbackContext obj)
        {
            doubleClickAddon.OnDeltaMove(obj.ReadValue<Vector2>());

        }

        public void OnClickStarted(InputAction.CallbackContext context)
        {
            RaycastHit hit;
            doubleClickAddon.OnClick();
            switch (doubleClickAddon.ClickCount())
            {
                case 1:

                    currentDragger = defautDraggable;

                    if (Physics.Raycast(
                            Camera.main.ScreenPointToRay(
                                Mouse.current.position.ReadValue()),
                            out hit))
                    {
                        IMouse dragger = hit.collider.gameObject.GetComponent<IMouse>();

                        if (dragger != null)
                        {
                            currentDragger = dragger;
                        }
                    }

                    currentDragger.OnInputMouseDown(context);
                    OnDragInPerformingWorldCoordinates += currentDragger.OnInputMouseDragScreenCoordinates;

                    break;

                case 2:

                    if (Physics.Raycast(
                            Camera.main.ScreenPointToRay(
                                Mouse.current.position.ReadValue()),
                            out hit))
                    {
                        IDoubleClick doubleClick = hit.collider.gameObject.GetComponent<IDoubleClick>();

                        doubleClick?.OnDoubleClick();
                    }

                    break;
            }
        }
        public void OnClickCanceled(InputAction.CallbackContext context)
        {
            OnDragInPerformingWorldCoordinates -= currentDragger.OnInputMouseDragScreenCoordinates;
            currentDragger.OnInputMouseUp(context);
        }


        private void Update()
        {
            if (!isApplicationInFocus) return;
            // idnno why but input system drag have some sensitivity issues .... or i stoooped
            // this will be fine by now
            // TouchDeltaMove_performed();
            Vector2 currentPosition = inputAction.TouchInput.PrimaryPosition.ReadValue<Vector2>();
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            if (!applicationWasUnfocusedOrStarted)
            {
                if (OnDragInPerformingWorldCoordinates != null)
                    Debug.Log("Catch");
                Vector2 delta = currentPosition - pastPointerValue;
                Vector3 worldDelta = screenToWorldConverter.FromScreenDeltaToWorldDelta(delta);
                if (OnDragInPerformingWorldCoordinates != null) Debug.Log($"Screen delta {delta} world delta {worldDelta}");

                OnDragInPerformingWorldCoordinates?.Invoke(worldDelta);
            }
            pastPointerValue = currentPosition;

            Vector2 onScreenEdge = Vector2.zero;
            if (currentPosition.y > screenSize.y - distanceFromEdge)
                onScreenEdge += Vector2.up;
            else if (currentPosition.y - distanceFromEdge < 0)
                onScreenEdge += Vector2.down;
            if (currentPosition.x > screenSize.x - distanceFromEdge)
                onScreenEdge += Vector2.right;
            else if (currentPosition.x - distanceFromEdge < 0)
                onScreenEdge += Vector2.left;
            if (onScreenEdge != Vector2.zero)
            {
                onEdgeOfScreenListerner?.CursorOnEdgeOfScreen(onScreenEdge);
                //    Debug.Log($"OnscreenEdge {onScreenEdge} {currentPosition} {screenSize}");
            }
            applicationWasUnfocusedOrStarted = false;
        }
        private void OnHoldPress(CallbackContext callbackContext)
        {
            // Debug.Log("Hoid");
        }

    }
}