using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Environment.Map
{
    public class MapCreator : MonoBehaviour
    {

        [SerializeField] Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] MapData mapDataAsset;
        [Zenject.Inject] TileMeshLibrary meshLibrary;
        [SerializeField] EditableTile editableTilePrefab;
        EditableTile[,] instantiatedTiles = new EditableTile[0, 0];


        private void Start()
        {
            LoadMap();
        }
        public void LoadMap()
        {
            if (!Application.isPlaying)
            {
                Debug.Log($"Unity is not playing silly");
                return;
            }
            Vector2Int gridSizeFromMapData = mapDataAsset.GetGridSize();
            Vector3 meshSize = editableTilePrefab.ScaledMeshSize;
            float xStart = (meshSize.x * (gridSizeFromMapData.x - 1)) / 2;
            float yStart = (meshSize.z * (gridSizeFromMapData.y - 1)) / 2;
            float tileElevation = 0.1f;
            Vector3 startPosition = new Vector3(-xStart, tileElevation, -yStart);
            Extensions.DisposeMatrix(instantiatedTiles);
            instantiatedTiles = new EditableTile[gridSizeFromMapData.x, gridSizeFromMapData.y];
            for (int x = 0; x < gridSizeFromMapData.x; x++)
            {
                for (int y = 0; y < gridSizeFromMapData.y; y++)
                {
                    mapDataAsset.TileGridCells[x, y].tileGridPosition =new Vector2Int(x, y);
                    instantiatedTiles[x, y] = Instantiate(editableTilePrefab, startPosition + new Vector3(meshSize.x * x, 0, meshSize.z * y), Quaternion.identity, this.transform);
                    instantiatedTiles[x, y].name = $"{x} {y} {instantiatedTiles[x, y].name} "; 
                    if (x == 0 && y == 0)
                    {
                        Debug.Log("catch");
                    }
                    ApplySettingsToTile(instantiatedTiles[x, y], mapDataAsset.TileGridCells[x, y]);
                   
                }
            }
        }
        public void UpdateTileMesh(string meshName, EditableTile editableTile)
        {
            Vector2Int index = GetTileIndexByTile(editableTile);
            mapDataAsset.TileGridCells[index].tileMeshName = meshName;
            ApplySettingsToTile(editableTile, mapDataAsset.TileGridCells[index]);
        }
        public void RotateTile(EditableTile editableTile)
        {
            Vector2Int index = GetTileIndexByTile(editableTile);
            mapDataAsset.TileGridCells[index].TileMeshRotation += 90;
            ApplySettingsToTile(editableTile, mapDataAsset.TileGridCells[index]);
        }
        private void ApplySettingsToTile(EditableTile editableTile, TileMapData tileMapData)
        {
            editableTile.SetMaterial(meshLibrary.MainPalette);
            editableTile.RotateTile(tileMapData.TileMeshRotation);
            Mesh mesh = meshLibrary.GetMeshByString(tileMapData.tileMeshName);
            editableTile.SetTileMesh(mesh);
        }
        private Vector2Int GetTileIndexByTile(EditableTile editableTile)
        {
            return instantiatedTiles.CoordinatesOfVector2(editableTile); 
        }

        public void RegenerateMap()
        {
            if (!Application.isPlaying)
            {
                Debug.Log($"Unity is not playing silly");
                return;
            }
            mapDataAsset.CreateTileMap(gridSize);
            LoadMap();
        }

    }
}
