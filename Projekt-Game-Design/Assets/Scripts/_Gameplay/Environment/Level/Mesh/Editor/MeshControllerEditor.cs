using UnityEditor;
using UnityEngine;

namespace MeshGenerator.Editor {
	[CustomEditor(typeof(MeshController))]
	public class MeshControllerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();
			
			var meshController = (MeshController) target;

			if ( GUILayout.Button("Create Mesh") ) {
				// string filePath = 
				// 	EditorUtility.SaveFilePanelInProject("Save Procedural Mesh", $"testMesh", "asset","");
				// AssetDatabase.CreateAsset(new Mesh(), filePath);
				
				meshController.CreateMesh();
			}
		}
	}
}