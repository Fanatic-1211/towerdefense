using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Game.Environment.Map
{
    [CustomEditor(typeof(MapCreator))]
    public class MapCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MapCreator myScript = (MapCreator)target;
            if (GUILayout.Button("RegenerateMap"))
            {
                myScript.RegenerateMap();
            }
            if (GUILayout.Button("LoadMap"))
            {
                myScript.LoadMap();
            }
        }

    }
}