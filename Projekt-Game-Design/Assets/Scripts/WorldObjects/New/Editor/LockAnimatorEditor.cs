using UnityEditor;
using UnityEngine;
using WorldObjects.Doors;

namespace Items.Editor {
	[CustomEditor(typeof(LockAnimator))]
	public class LockAnimatorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			DrawDefaultInspector();
            
			var lockAnimator = (LockAnimator) target;

			if (GUILayout.Button("Open Lock")) {  
				// call on button click
				lockAnimator.OpenLock();
			}
		}
	}
}