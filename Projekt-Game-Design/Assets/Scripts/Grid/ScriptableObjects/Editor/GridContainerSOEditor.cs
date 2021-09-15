using UnityEditor;
using UnityEngine;

namespace Grid.Editor {
    [CustomEditor(typeof(GridContainerSO))]
    public class GridContainerSOEditor : UnityEditor.Editor {
        
        public override void OnInspectorGUI() {

            DrawDefaultInspector();

            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) {
            
                using (new GUILayout.HorizontalScope()) {
                    GUILayout.Label("test", GUILayout.Width( 60 ));
                    GUILayout.Label("test2");
                }
                
            }
            
        }
    }
}