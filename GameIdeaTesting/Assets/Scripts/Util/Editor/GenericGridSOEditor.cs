using UnityEditor;
using UnityEngine;
using Util;

[CustomEditor(typeof(GenericGridSO))]
public class GenericGridSOEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        var gridSO = (GenericGridSO)target;

        if (GUILayout.Button("toggleDebug")) {
            gridSO.ToggleShowDebug();
        }
    }
}
