using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Game.Environment.Map
{
    public class EditableTile : MonoBehaviour, ISelectable
    {
        [SerializeField] MeshFilter meshFilter;
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] MeshRendererHighlight selectable;
        private event Action<bool> TileWasSelected;
        public Vector3 ScaledMeshSize => Vector3.Scale(meshFilter.sharedMesh.bounds.size, meshFilter.transform.lossyScale);
        public void SetTileMesh(Mesh mesh)
        {
            meshFilter.mesh = mesh;
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.sharedMaterial = material;
        }
        public void RotateTile(int rotaion)
        {
            meshRenderer.transform.localEulerAngles = new Vector3(0, rotaion);
        }

        public Action<bool> TileSelected() => TileWasSelected;

        public void SelectTarget()
        {
            selectable.SelectTarget();
            TileWasSelected?.Invoke(true);
        }

        public void DeselectTarget()
        {
            selectable.DeselectTarget();
            TileWasSelected?.Invoke(false);
        }
    }
}