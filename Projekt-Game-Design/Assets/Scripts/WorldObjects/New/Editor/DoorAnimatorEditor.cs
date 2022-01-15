using UnityEditor;
using UnityEngine;
using WorldObjects.Doors;

namespace Items.Editor {
	[CustomEditor(typeof(DoorAnimator))]
	public class DoorAnimatorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			DrawDefaultInspector();
            
			var doorAnimator = (DoorAnimator) target;

			if (GUILayout.Button("Open Door")) {  
				// call on button click
				doorAnimator.OpenDoor();
			}
		}
	}
}