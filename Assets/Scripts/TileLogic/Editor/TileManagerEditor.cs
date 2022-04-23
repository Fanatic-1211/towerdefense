using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Game.Environment.Tile { 
[CustomEditor(typeof(TileManager))]
public class TileManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TileManager myTarget = (TileManager)target;
        DrawDefaultInspector();
        if (GUILayout.Button("RebuildPath"))
        {
            myTarget.RebuildPath();
        }
    }
}
}
