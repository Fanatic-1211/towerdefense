using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Environment.Map
{
    public class MapCreator : MonoBehaviour
    {
     
        [SerializeField] Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] MapData mapDataAsset;
        [SerializeField] TileMeshLibrary meshLibrary;

        public void InitializeMap()
        {
            
        }
        
        public void RegenerateMap()
        {
            mapDataAsset.CreateTileMap(gridSize);
        }
    }
}
