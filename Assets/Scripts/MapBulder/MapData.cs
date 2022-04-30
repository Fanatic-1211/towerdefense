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
        public string tileMeshName;
        public int tileMeshRotation;
        public Vector2Int tileGridPosition;
        public TileType tileType;
    }
    public class TileObstacle
    {
        public string tileObstacleName;
    }
    [System.Serializable]
    public class MapData
    {
        [JsonProperty] [SerializeField] TileMapData[,] tileMapDatas = new TileMapData[10, 10];
        [JsonProperty] [SerializeField] string test = "A";
        
        public MapData()
        {
            for (int i = 0; i < tileMapDatas.GetLength(0); i++)
            {
                for (int i1 = 0; i1 < tileMapDatas.GetLength(1); i1++)
                {
                    TileMapData tileMap = new TileMapData();
                    tileMap.tileMeshName = "aaa"+i1.ToString()+i.ToString();
                    tileMap.tileMeshRotation = 0;
                    tileMapDatas[i, i1] = tileMap;
                }
            }
        }
    }
}
