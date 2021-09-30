using Grid;
using UnityEditor;
using UnityEngine;

namespace GDP01.Grid.Editor {
    [CustomEditor(typeof(GridController))]
    public class GridControllerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var testGrid = (GridController) target;
            
            
        }
    }
}