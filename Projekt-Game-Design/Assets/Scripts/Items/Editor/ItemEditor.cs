using UnityEditor;
using UnityEngine;

namespace Items.Editor {
	[CustomEditor(typeof(Item))]
	public class ItemEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {

			DrawDefaultInspector();
            
			var item = (Item) target;

			if (GUILayout.Button("Reset")) {  
				// call on button click
				item.Reset();
			}
		}
	}
}
