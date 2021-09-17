﻿using UnityEditor;
using UnityEngine;
using Graph;

namespace GDP01.Graph.Editor {
    [CustomEditor(typeof(GraphGenerator))]
    public class GraphGeneratorEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var graphGenerator = (GraphGenerator) target;

            if (GUILayout.Button("Generate Graph")) {
                graphGenerator.GenerateGraphFromGrids();
            }
            
        }
    }
}