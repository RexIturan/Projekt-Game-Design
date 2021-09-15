using UnityEditor;
using UnityEngine;

namespace Test.Editor {
    [CustomEditor(typeof(TestGrid))]
    public class TestGridEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            TestGrid testGrid = (TestGrid) target;
            
            
            if (GUILayout.Button("toggleDebug")) {
                testGrid.CreateGrid();
            }
            
        }
    }
}