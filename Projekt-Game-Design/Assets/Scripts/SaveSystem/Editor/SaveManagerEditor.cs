using UnityEditor;
using UnityEngine;

namespace SaveLoad.Editor {
    [CustomEditor(typeof(SaveManager))]
    public class SaveManagerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {

            DrawDefaultInspector();

            var saveManager = (SaveManager) target;
            
            if (GUILayout.Button("SaveGrid")) {  
                saveManager.SaveGridContainer(); 
                AssetDatabase.Refresh();
            }
            
            if (GUILayout.Button("LoadGrid")) {  
                saveManager.LoadGridContainer(); 
                AssetDatabase.Refresh();
            }
            
            if (GUILayout.Button("LoadLevel")) {  
                saveManager.InitializeLevel(); 
                AssetDatabase.Refresh();
            }
        }
    }
}