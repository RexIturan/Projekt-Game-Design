using Grid;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace GDP01.Grid.Editor {
    [CustomEditor(typeof(GridContainerSO))]
    public class GridContainerSOEditor : UnityEditor.Editor {

        private SerializedObject _so;

        private void OnEnable() {
            _so = serializedObject;
            //propStuff = so.FindProperty("");
        }
        
        public override void OnInspectorGUI() {
        
            _so.Update();
            // EditorGUILayout.PropertyField( propStuff );
            if (_so.ApplyModifiedProperties()) {
                // true if something changed
            }
            
            DrawDefaultInspector();
        
            // var container = (GridContainerSO) target;
            
            // styling
            // using (new GUILayout.VerticalScope(EditorStyles.helpBox)) {
            //
            //     using (new GUILayout.HorizontalScope()) {
            //         GUILayout.Label("test", GUILayout.Width( 60 ));
            //         GUILayout.Label("test2");
            //     }
            // }
        }

    }
}