using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Environment.Map
{
    public class TileMapEditorDraggable : MonoBehaviour
    {
        private ISelectable currentTile;
        public ISelectable CurrentTile => currentTile;
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position,Vector3.down,out hit ))
            {
                ISelectable tileSelectable = hit.collider.gameObject.GetComponent<ISelectable>();
              
                if (tileSelectable != null&& currentTile!=tileSelectable)
                {
                    currentTile?.DeselectTarget();
                    currentTile = tileSelectable;
                    currentTile.SelectTarget();
                }
            }

        }
        private void OnDestroy()
        {
            currentTile?.DeselectTarget();
        }
    }
}