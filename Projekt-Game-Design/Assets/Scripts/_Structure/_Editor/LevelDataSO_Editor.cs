using UnityEditor;
using UnityEngine;

namespace GDP01.Structure._Editor {
	[CustomEditor(typeof(LevelDataSO))]
	public class LevelDataSO_Editor : Editor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			var levelData = ( LevelDataSO )target;
			
			// Add Connector
			if (GUILayout.Button("Add Connector")) {
				levelData.AddNewConnector();
			}
		}
	}
}