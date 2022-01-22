using UnityEditor;
using UnityEngine;

namespace UI.Controller.Editor {
	[CustomEditor(typeof(ActionBarController))]
	public class ActionBarControllerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();

			var actionBarController = ( ActionBarController )target;

			if ( GUILayout.Button("Test Move Navigation") ) {
				actionBarController.FocusThroughAllButtons();
			}
			
			if ( GUILayout.Button("Test Submit Navigation") ) {
				actionBarController.PressAllButtons();
			}
		}
	}
}