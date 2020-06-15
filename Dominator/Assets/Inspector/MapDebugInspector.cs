using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapDebugInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGenerator mg = (MapGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            mg.initialize();
            mg.generate();
        }
        if (GUILayout.Button("Reset"))
        {
        }
    }
}
