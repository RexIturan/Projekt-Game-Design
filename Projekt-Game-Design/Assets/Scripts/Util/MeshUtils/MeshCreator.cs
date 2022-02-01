using UnityEditor;
using UnityEngine;

namespace GDP01.Util.MeshUtils {
	public static class MeshCreator {
		public static Mesh CreateMesh(string name) {
			var mesh = new Mesh();
#if UNITY_EDITOR
			// string filePath = 
			// 	EditorUtility.SaveFilePanelInProject("Save Procedural Mesh", $"{name}", "asset", "");
			string filePath = $"Assets/Art/Models/level/{name}.asset";
			AssetDatabase.CreateAsset(mesh, filePath);
#endif
			return mesh;
		}
	}
}