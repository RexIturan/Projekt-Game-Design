using UnityEditor;
using UnityEngine;

namespace GDP01.UI.Structure.Editor {
	[CustomEditor(typeof(ScreenManager))]
	public class ScreenManagerEditor : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();

			var screenManager = ( ScreenManager )target;

			//Draw SceneArray
			
			if ( GUILayout.Button("Update Screens") ) {
				screenManager.ResetScreens();
				screenManager.UpdateScreens();
			}
		}
	}
}