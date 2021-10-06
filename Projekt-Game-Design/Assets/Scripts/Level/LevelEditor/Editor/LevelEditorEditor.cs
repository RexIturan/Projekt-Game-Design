using UnityEditor;
using UnityEngine;

namespace LevelEditor.Editor {
    [CustomEditor(typeof(LevelEditor))]
    public class LevelEditorEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {

            DrawDefaultInspector();
            
            var levelEditor = (LevelEditor) target;

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