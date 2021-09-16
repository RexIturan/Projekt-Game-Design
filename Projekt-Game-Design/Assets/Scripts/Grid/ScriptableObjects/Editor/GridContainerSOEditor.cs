using System;
using UnityEditor;
using UnityEngine;

namespace Grid.Editor {
    [CustomEditor(typeof(GridContainerSO))]
    public class GridContainerSOEditor : UnityEditor.Editor {

        private SerializedObject so;

        private void OnEnable() {
            so = serializedObject;
            //propStuff = so.FindProperty("");
        }

        public override void OnInspectorGUI() {

            so.Update();
            // EditorGUILayout.PropertyField( propStuff );
            if (so.ApplyModifiedProperties()) {
                // true if something changed
            }
            
            
            DrawDefaultInspector();

            var container = (GridContainerSO) target;
            
            // styling
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) {
            
                using (new GUILayout.HorizontalScope()) {
                    GUILayout.Label("test", GUILayout.Width( 60 ));
                    GUILayout.Label("test2");
                }
            }
        }

    }
}