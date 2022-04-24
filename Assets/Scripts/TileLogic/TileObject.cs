using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment.Tile
{
    public class TileObject : MonoBehaviour, ITowerPlaceable
    {
        Dictionary<TileType, Color> tileGizmosColor = new Dictionary<TileType, Color>()
        {
            { TileType.finish,Color.black},
            { TileType.spawn,Color.white},
            { TileType.path,Color.yellow},
            { TileType.placeable,Color.green},
             { TileType.locked,Color.magenta}

        };
        public TileType TileType => tileType;
        [SerializeField] TileType tileType;
        [SerializeField] SpriteRenderer addRenderer;
        [SerializeField] MeshFilter meshFilter;
        public void SetMeshView(Mesh mesh,float rotation)
        {
            meshFilter.mesh = mesh;
            meshFilter.transform.localEulerAngles = new Vector3(0, rotation);
        }
        private GameObject occupation;
        public void SetOccupation(GameObject occupation)
        {
            this.occupation = occupation;
        }
        public bool IsCellAvalable() => occupation == null;
        public T PlaceTower<T>(T towerPrefab) where T : MonoBehaviour
        {
            if (!IsCellAvalable())
                return null;
            T obj = Instantiate(towerPrefab, this.transform.position, Quaternion.identity, this.transform);
            occupation = obj.gameObject;
            return obj;
        }

        public void HighlightPlace(bool highLight)
        {
            addRenderer.color = IsCellAvalable() ? Color.white : Color.red;
            addRenderer.gameObject.SetActive(highLight);
        }

        public void ClearCell()
        {
            if (occupation != null)
                Destroy(occupation);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = tileGizmosColor[tileType];
            Gizmos.DrawSphere(this.transform.position, 1);
        }
    }
}