using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameSystem.Input
{
    public interface IMouse
    {
        void OnMouseDown(InputAction.CallbackContext context);
        void OnMouseDrag(Vector2 delta);
        void OnMouseUp(InputAction.CallbackContext context);
    }
}