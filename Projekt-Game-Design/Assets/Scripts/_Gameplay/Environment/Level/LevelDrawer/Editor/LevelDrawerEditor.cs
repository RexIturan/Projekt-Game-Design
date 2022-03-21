using UnityEditor;
using UnityEngine;
using Visual;

namespace Level.Visual.Editor {
    [CustomEditor(typeof(LevelDrawer))]
    public class LevelDrawerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {

            DrawDefaultInspector();
            
            var levelEditor = (LevelDrawer) target;

            if (GUILayout.Button("Redraw Level")) {  
                // call on button click
                levelEditor.RedrawLevel();  
            }
        }
    }
}