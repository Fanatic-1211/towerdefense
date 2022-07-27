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
        private event Action<Vector2> OnDragInActionScreenCoordinates;
        private event Action<Vector2> OnDragInActionWorldCoordinates;
        [SerializeField] CameraAddon cameraController;
        IMouse defautDraggable => cameraController;
        IMouse currentDragger;
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

                    currentDragger.OnMouseDown(context);
                    OnDragInActionScreenCoordinates += currentDragger.OnMouseDrag;

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
            OnDragInActionScreenCoordinates -= currentDragger.OnMouseDrag;
            currentDragger.OnMouseUp(context);
        }


        private void Update()
        {
            // idnno why but input system drag have some sensitivity issues .... or i stoooped
            // this will be fine by now
           // TouchDeltaMove_performed();
            Vector2 currentPosition = inputAction.TouchInput.PrimaryPosition.ReadValue<Vector2>(); 
            Vector2 delta = currentPosition- pastPointerValue; 
            OnDragInActionScreenCoordinates?.Invoke(delta);
            pastPointerValue = currentPosition;
            Debug.Log($"DeltaValue {delta}");
        }
        private void OnHoldPress(CallbackContext callbackContext)
        {
           // Debug.Log("Hoid");
        }



    }
}