using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Environment.Tile;
using Newtonsoft.Json;

namespace Game.Environment.Map
{
    [System.Serializable]
    public class TileMapData
    {
        public string tileMeshName = string.Empty;
        [SerializeField] private int tileMeshRotation;
        public int TileMeshRotation { get => tileMeshRotation; set { tileMeshRotation = value > 360 ? 0 : value; } }
        public Vector2Int tileGridPosition;
        public TileType tileType;
    }
    public class TileObstacle
    {
        public string tileObstacleName;
    }

    [CreateAssetMenu(fileName = "NewMapData", menuName = "CreateMapData")]
    public class MapData : ScriptableObject, IMapData
    {
        [SerializeField] SMatrix<TileMapData> tileMapDatas;
        public SMatrix<TileMapData> GetTileGridCells() => tileMapDatas;
        public void CreateTileMap(Vector2Int mapSize)
        {
            tileMapDatas = new SMatrix<TileMapData>(mapSize);
        }
        public Vector2Int GetGridSize() => tileMapDatas.MatrixSize;
    }
    [System.Serializable]
    public class MapDataJson : IMapData
    {
        [SerializeField] SMatrix<TileMapData> tileMapDatas;
        public SMatrix<TileMapData> GetTileGridCells() => tileMapDatas;
        public void CreateTileMap(Vector2Int mapSize)
        {
            tileMapDatas = new SMatrix<TileMapData>(mapSize);
        }
        public Vector2Int GetGridSize() => tileMapDatas.MatrixSize;
        public MapDataJson()
        {
            CreateTileMap(new Vector2Int(0, 0));
        }
    }
    public interface IMapData
    {
        public SMatrix<TileMapData> GetTileGridCells();
        public void CreateTileMap(Vector2Int mapSize);
        public Vector2Int GetGridSize();
    }
}
