using UnityEditor;
using UnityEngine;

namespace Items.Editor {
	[CustomEditor(typeof(ItemComponent))]
	public class ItemEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {

			DrawDefaultInspector();
            
			var item = (ItemComponent) target;

			if (GUILayout.Button("Reset")) {  
				// call on button click
				item.Reset();
			}
		}
	}
}
