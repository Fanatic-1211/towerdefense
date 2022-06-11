using Game.Environment.Tile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[CreateAssetMenu(fileName = "MapData", menuName = "TileMeshCollection")]
public class TileMeshLibrary : ScriptableObject
{
    [System.Serializable]
    public class TileMesh
    {
        public Sprite meshPreview;
        public string meshName;
        public Mesh Mesh;
        public TileType tileType;
    }
    [SerializeField] private List<TileMesh> tileMeshCollection = new List<TileMesh>();
    [SerializeField] private Mesh fallbackMesh;
    [SerializeField] private Material mainPalette;
    public Material MainPalette => mainPalette;
    Dictionary<string, Mesh> tileMeshDictionary = new Dictionary<string, Mesh>();
    
    public Mesh GetMeshByString(string meshName)
    {
        IsDictionaryBuilded();
        if (tileMeshDictionary.ContainsKey(meshName))
            return tileMeshDictionary[meshName];
        return fallbackMesh;
    } 
    private bool IsDictionaryBuilded(bool buildDictionary = true)
    {
        if (tileMeshDictionary.Count <= 0)
        {
            if (buildDictionary) 
            {
                BuildDictionary();
                return true;
            }
            return false;
        }
        return true;
    }
    private void BuildDictionary()
    {
        tileMeshDictionary.Clear();
        Debug.Log("Building Dictionary");
        foreach (var item in tileMeshCollection)
        { 
            tileMeshDictionary[item.meshName] = item.Mesh;
        }
    }
    public List<TileMesh> GetAllTileMeshes()
    {
        var output = new List<TileMesh>();
        output.AddRange(tileMeshCollection);
        return output; 
    }
    private void OnValidate()
    {
        if(tileMeshCollection.Count!= tileMeshDictionary.Count)
        {
            Debug.Log($"List collection count {tileMeshCollection.Count} was not equal { tileMeshDictionary.Count}{ Environment.NewLine} rebuilding Dictionary");
            BuildDictionary();
        }
        else
        {
            Debug.Log($"Dictionary check passed. Current collectionCount is {tileMeshDictionary.Count}");
        }
    }
}
