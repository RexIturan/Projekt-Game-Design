using UnityEditor;
using UnityEngine;
using WorldObjects.Doors;

namespace Items.Editor {
	[CustomEditor(typeof(SlidingBlockAnimator))]
	public class SlidingBlockAnimatorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			DrawDefaultInspector();
            
			var doorAnimator = (SlidingBlockAnimator) target;

			if (GUILayout.Button("Start Slide")) {  
				// call on button click
				doorAnimator.StartAnimation();
			}
		}
	}
}