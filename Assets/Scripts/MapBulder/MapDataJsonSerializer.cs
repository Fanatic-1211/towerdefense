using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Game.Environment.Map
{
    public class MapDataJsonSerializer : MapDataSerializerBase
    {
        //  [SerializeField] TextAsset textAsset;
        string serializeRootFolder = "Assets\\Resources";
        string mapDataPath = "MapData\\DevMap.json";
        MapDataJson mapData;
        public override IMapData GetMapData()
        {
            if (mapData == null)
            {
                mapData = DeserializeMap();
                Debug.Log(mapData.GetTileGridCells()[0, 0].tileMeshName);
            }
            return mapData;
        }

        public override void SerializeMap()
        {
            SerializeMapData(mapData);
        }

        private MapDataJson DeserializeMap()
        {
            Debug.Log("Map deserialized");
            MapDataJson myObject = null;
            try
            {
                string fullPath = Path.Combine(serializeRootFolder, mapDataPath);
                StreamReader streamReader = new StreamReader(fullPath);
                myObject = JsonUtility.FromJson<MapDataJson>(streamReader.ReadToEnd());
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
                using StreamWriter writer = new StreamWriter(fullPath, false);
                writer.AutoFlush = true;
                Debug.Log(json);
                writer.Write(json);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception exc)
            {
                Debug.LogError($"Can't serialize! map due to exception {exc.Message}. ");
            }

        }
    }

}