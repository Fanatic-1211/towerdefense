using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Game.Environment.Map
{
    public class MapDataSerializer : MonoBehaviour
    {
        //  [SerializeField] TextAsset textAsset;
        string serializeRootFolder = "Assets\\Resources";
        string mapDataPath = "MapData\\DevMap.json";
        MapDataJson mapData;
        public IMapData GetMapData()
        {
            if (mapData == null)
            {
                mapData = DeserializeMap();
              Debug.Log(mapData.GetTileGridCells()[0, 0].tileMeshName);
            }
            return mapData;
        }
        public void SerializeMap()
        {
            SerializeMapData(mapData);
        }
        private MapDataJson DeserializeMap()
        {
            Debug.Log("Map deserialized");
            MapDataJson myObject = null;
            try
            {
                Debug.Log($"Deserializing obj from {mapDataPath}");
                TextAsset textFile = Resources.Load(mapDataPath.Replace(".json","")) as TextAsset;
                Debug.Log(textFile);
                myObject = JsonUtility.FromJson<MapDataJson>(textFile.text);
            }
            catch (Exception exc)
            {
                Debug.LogWarning($"Can't deserialize map due to exception {exc.Message} {System.Environment.NewLine}Stack trace {exc.StackTrace}");
                myObject = new MapDataJson();
                SerializeMapData(myObject);
            }
            return myObject;
        }
        private void SerializeMapData(MapDataJson _mapData)
        {
            if (!Application.isEditor)
            {
                Debug.LogError($"Should not be called from BUILD!");
                return;
            }
            try
            {
                string fullPath = Path.Combine(serializeRootFolder, mapDataPath);
                Debug.Log($"Serializing obj to {fullPath}");
                string json = JsonUtility.ToJson(_mapData, true);
                StreamWriter writer = new StreamWriter(fullPath, false);
                writer.Write(json);
                writer.Close();
            }
            catch (Exception exc)
            {
                Debug.LogError($"Can't serialize! map due to exception {exc.Message}. ");
            }

        }
    }//Load a text file (Assets/Resources/Text/textFile01.txt)

}