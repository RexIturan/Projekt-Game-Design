using UnityEditor;
using UnityEngine;
using WorldObjects.Doors;

namespace Items.Editor {
	[CustomEditor(typeof(KeyLockAnimator))]
	public class KeyLockAnimatorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
            
			var lockAnimator = (KeyLockAnimator) target;

			if (GUILayout.Button("Open Lock")) {  
				// call on button click
				lockAnimator.OpenLock();
			}

			if (GUILayout.Button("Reset")) {  
				// call on button click
				lockAnimator.Reset();
			}
		}
	}
}
