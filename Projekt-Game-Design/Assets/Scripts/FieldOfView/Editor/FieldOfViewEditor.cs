using Graph;
using UnityEditor;
using UnityEngine;

namespace field_of_view.Editor {
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var fieldOfViewGenerator = (FieldOfView) target;

            if (GUILayout.Button("generateVisionString")) {
                fieldOfViewGenerator.generateVision();
            }
            
        }
    }
}