using UnityEditor;
using UnityEngine;
using WorldObjects;

namespace Items.Editor {
	[CustomEditor(typeof(SwitchAnimator))]
	public class SwitchAnimatorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {

			DrawDefaultInspector();
            
			var switchAnimator = (SwitchAnimator) target;

			if (GUILayout.Button("Flip")) {  
				// call on button click
				switchAnimator.FlipSwitch();
			}
		}
	}
}
