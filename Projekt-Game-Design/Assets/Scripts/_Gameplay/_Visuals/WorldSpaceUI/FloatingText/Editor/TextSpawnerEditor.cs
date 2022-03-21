using UnityEditor;
using UnityEngine;

namespace GDP01._Gameplay._Visuals.WorldSpaceUI.FloatingText.Editor {
	[CustomEditor(typeof(TextSpawner))]
	public class TextSpawnerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();

			var textSpawner = ( TextSpawner )target;
			
			if ( GUILayout.Button("Spawn Flaoting Text") ) {
				textSpawner.SpawnTextMessage(
					textSpawner.SpawnText, textSpawner.gameObject.transform.position, textSpawner.SpawnColor);
			}
		}
	}
}