using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

namespace Game.Environment.Map
{
    public class MapCreator : MonoBehaviour
    {

        [SerializeField] Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] MapDataSerializer mapDataSerializer;
        IMapData iMapDataAsset;
        [Zenject.Inject] TileMeshLibrary meshLibrary;
        [SerializeField] EditableTile editableTilePrefab;
        EditableTile[,] instantiatedTiles = new EditableTile[0, 0];
        private void Awake()
        {
            iMapDataAsset = mapDataSerializer.GetMapData();
        }
        private void OnApplicationQuit()
        {
            mapDataSerializer.SerializeMap();
        }
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
            Vector2Int gridSizeFromMapData = iMapDataAsset.GetGridSize();
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
                    iMapDataAsset.GetTileGridCells()[x, y].tileGridPosition = new Vector2Int(x, y);
                    instantiatedTiles[x, y] = Instantiate(editableTilePrefab, startPosition + new Vector3(meshSize.x * x, 0, meshSize.z * y), Quaternion.identity, this.transform);
                    instantiatedTiles[x, y].name = $"{x} {y} {instantiatedTiles[x, y].name} ";
                    ApplySettingsToTile(instantiatedTiles[x, y], iMapDataAsset.GetTileGridCells()[x, y]);

                }
            }
        }
        public void UpdateTileMesh(string meshName, EditableTile editableTile)
        {
            Vector2Int index = GetTileIndexByTile(editableTile);
            iMapDataAsset.GetTileGridCells()[index].tileMeshName = meshName;
            ApplySettingsToTile(editableTile, iMapDataAsset.GetTileGridCells()[index]);
        }
        public void RotateTile(EditableTile editableTile)
        {
            Vector2Int index = GetTileIndexByTile(editableTile);
            iMapDataAsset.GetTileGridCells()[index].TileMeshRotation += 90;
            ApplySettingsToTile(editableTile, iMapDataAsset.GetTileGridCells()[index]);
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
            iMapDataAsset.CreateTileMap(gridSize);
            LoadMap();
        }

    }
}
