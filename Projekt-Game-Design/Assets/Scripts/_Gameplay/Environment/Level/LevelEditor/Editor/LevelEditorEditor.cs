using UnityEditor;
using UnityEngine;

namespace Level.LevelEditor.Editor {
    [CustomEditor(typeof(global::LevelEditor.LevelEditor))]
    public class LevelEditorEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {

            DrawDefaultInspector();
            
            var levelEditor = (global::LevelEditor.LevelEditor) target;

            if (GUILayout.Button("ResetLevel")) {  
                // call on button click
                levelEditor.ResetLevel();  
            }
            
            if (GUILayout.Button("Redraw Level")) {  
                // call on button click
                levelEditor.RedrawLevel();  
            }
            
        }
    }
}