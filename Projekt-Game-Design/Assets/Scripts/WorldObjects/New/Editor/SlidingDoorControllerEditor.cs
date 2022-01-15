using UnityEditor;
using UnityEngine;
using WorldObjects.Doors;

namespace Items.Editor {
	[CustomEditor(typeof(SlidingDoorController))]
	public class SlidingDoorControllerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			DrawDefaultInspector();
            
			var slidingDoorController = (SlidingDoorController) target;

			if (GUILayout.Button("Initialise")) {  
				// call on button click
				slidingDoorController.InitSlidingDoor();
			}
			
			if (GUILayout.Button("Open Door")) {  
				// call on button click
				slidingDoorController.OpenDoor();
			}
			
			if (GUILayout.Button("Reset")) {  
				// call on button click
				slidingDoorController.Reset();
			}
		}
	}
}