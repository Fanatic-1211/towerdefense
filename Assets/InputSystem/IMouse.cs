﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.Input
{
    public interface IMouse
    {
        void OnInputMouseDown(InputAction.CallbackContext context);
        void OnInputMouseDragWorldCoordinates(Vector3 delta);
        void OnInputMouseUp(InputAction.CallbackContext context);
    }
}