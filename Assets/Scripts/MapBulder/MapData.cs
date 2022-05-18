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
    public class MapData : ScriptableObject
    {
        [SerializeField] SMatrix<TileMapData> tileMapDatas;
        public SMatrix<TileMapData> TileGridCells => tileMapDatas;
        public void CreateTileMap(Vector2Int mapSize)
        {
            tileMapDatas = new SMatrix<TileMapData>(mapSize);
        }
        public Vector2Int GetGridSize()=> tileMapDatas.MatrixSize;
    }
}
