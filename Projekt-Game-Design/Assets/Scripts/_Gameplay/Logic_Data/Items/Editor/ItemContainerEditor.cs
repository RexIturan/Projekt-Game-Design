using UnityEditor;
using UnityEngine;

namespace Items.Editor {
	[CustomEditor(typeof(ItemContainerSO))]
	public class ItemContainerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();
			
			var container = (ItemContainerSO) target;
			container.UpdateItemList();
			
			if (GUILayout.Button("Update Item Dict")) {  
				// call on button click
				container.UpdateItemList();
			}
		}
	}
}