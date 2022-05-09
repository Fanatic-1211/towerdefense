using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Environment.Map
{
    public class EditableTile : MonoBehaviour
    {
        [SerializeField] MeshFilter meshFilter;
        [SerializeField] MeshRenderer meshRenderer;
        public Vector3 ScaledMeshSize => Vector3.Scale(meshFilter.sharedMesh.bounds.size, meshFilter.transform.lossyScale);
        public void SetTileMesh(Mesh mesh)
        {
            meshFilter.mesh = mesh;
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
        
    }
}