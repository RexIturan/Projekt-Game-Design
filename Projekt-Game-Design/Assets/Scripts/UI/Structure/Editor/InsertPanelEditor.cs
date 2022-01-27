using UnityEditor;
using UnityEngine;

namespace GDP01.UI.Structure.Editor {
	[CustomEditor(typeof(InsertPanel))]
	public class InsertPanelEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();

			var subPanel = ( InsertPanel )target;
			
			GUILayout.BeginHorizontal();
			
			if ( GUILayout.Button("Init") ) {
				subPanel.Init();
			}
			
			if ( GUILayout.Button("Reset") ) {
				subPanel.Reset();
			}
			
			GUILayout.EndHorizontal();
		}
	}
}