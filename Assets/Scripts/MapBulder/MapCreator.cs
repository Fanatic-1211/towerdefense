using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Environment.Map
{
    public class MapCreator : MonoBehaviour
    {

        [SerializeField] Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] MapData mapDataAsset;
        [Zenject.Inject] TileMeshLibrary meshLibrary;
        [SerializeField] EditableTile editableTilePrefab;
        EditableTile[,] instantiatedTiles = new EditableTile[0, 0];

        public void LoadMap()
        {
            Vector2Int gridSizeFromMapData = mapDataAsset.GetGridSize();
            Vector3 meshSize = editableTilePrefab.ScaledMeshSize;
            float xStart = (meshSize.x * (gridSizeFromMapData.x - 1)) / 2;
            float yStart = (meshSize.z * (gridSizeFromMapData.y - 1)) / 2;
            Vector3 startPosition = new Vector3(-xStart, 0, -yStart);
            Extensions.DisposeMatrix(instantiatedTiles);
            instantiatedTiles = new EditableTile[gridSizeFromMapData.x, gridSizeFromMapData.y];
            for (int x = 0; x < gridSizeFromMapData.x; x++)
            {
                for (int y = 0; y < gridSizeFromMapData.y; y++)
                {
                    instantiatedTiles[x, y] = Instantiate(editableTilePrefab, startPosition + new Vector3(meshSize.x * x, 0, meshSize.z * y), Quaternion.identity, this.transform);
                    instantiatedTiles[x, y].SetMaterial(meshLibrary.MainPalette);
                    Mesh mesh = meshLibrary.GetMeshByString(mapDataAsset.TileGridCells[x, y].tileMeshName);
                    instantiatedTiles[x, y].SetTileMesh(mesh);
                }
            }
        }

        public void RegenerateMap()
        {
            mapDataAsset.CreateTileMap(gridSize);
            LoadMap();
        }
        
    }
}
