using UnityEditor;
using UnityEngine;

namespace SaveSystem.Editor {
	[CustomEditor(typeof(SaveManager))]
	public class SaveManagerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			DrawDefaultInspector();

			var saveManager = ( SaveManager )target;

			if ( GUILayout.Button("Save Level") ) {
				saveManager.SaveLevel("");
				AssetDatabase.Refresh();
			}

			if ( GUILayout.Button("Load Level") ) {
				saveManager.LoadLevel("");
				saveManager.InitializeLevel();
				AssetDatabase.Refresh();
			}

			if ( GUILayout.Button("Reload Level") ) {
				//todo create new savegame object??
				saveManager.InitializeLevel();
				AssetDatabase.Refresh();
			}
		}
	}
}