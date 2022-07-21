using UnityEngine.InputSystem;

namespace Game.GameSystem.Input
{
    public interface IMouse
    {
        void OnMouseDown(InputAction.CallbackContext context);
        void OnMouseDrag(InputAction.CallbackContext context);
        void OnMouseUp(InputAction.CallbackContext context);
    }
}