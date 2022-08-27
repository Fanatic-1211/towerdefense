using Game.GameSystem.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Environment.Map
{
    public class TileMapEditorSpawner : MonoBehaviour, IMouse
    {
        [SerializeField] TileMapEditorDraggable tileDraggable;
        private TileMapEditorDraggable currentDraggable;
        [SerializeField] string tileMeshName;
        [Inject] MapCreator mapCreator;
        private void CreateDraggableTile()
        {
            if (currentDraggable != null) { 
                Debug.LogWarning($"Trying create draggable that already exist! This should not be happen");
            }
            currentDraggable = Instantiate(tileDraggable, this.transform.position, Quaternion.identity, this.transform);
        }
        public void OnInputMouseDown(InputAction.CallbackContext context)
        {
            CreateDraggableTile();
        }

        public void OnInputMouseDragWorldCoordinates(Vector3 delta)
        {
            currentDraggable.transform.position += delta;
        }

        public void OnInputMouseUp(InputAction.CallbackContext context)
        {
            mapCreator.UpdateTileMesh(tileMeshName, currentDraggable.CurrentTile);
            Destroy(currentDraggable.gameObject);
        }
    }
}