using LevelEditor;
using UnityEditor;
using UnityEngine;

namespace Level.LevelEditor.Editor {
	[CustomEditor(typeof(PreviewBlockController))]
	public class PreviewBlockControllerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();
			
			var previewBlock = (PreviewBlockController) target;

			if (GUILayout.Button("Update Preview Material")) {
				// call on button click				
				previewBlock.UpdatePreviewColor();
			}
		}
	}
}