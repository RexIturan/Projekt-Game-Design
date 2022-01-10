using UnityEditor;
using UnityEngine;

namespace FieldOfView.Editor {
    [CustomEditor(typeof(FieldOfViewController))]
    public class FieldOfViewEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var fieldOfViewGenerator = (FieldOfViewController) target;

            if (GUILayout.Button("generateVisionString")) {
                fieldOfViewGenerator.GenerateVision();
            }
            
            if (GUILayout.Button("generate Player Char Vision")) {
	            fieldOfViewGenerator.GeneratePlayerCharacterVision();
            }
        }
    }
}