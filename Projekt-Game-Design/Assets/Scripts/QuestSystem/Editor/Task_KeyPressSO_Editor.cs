using QuestSystem.ScriptabelObjects;
using UnityEditor;
using UnityEngine;

namespace QuestSystem.Editor {
	[CustomEditor(typeof(Task_KeyPress_SO))]
	public class Task_KeyPressSO_Editor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			DrawDefaultInspector();
            
			var taskKeyPressSO = (Task_KeyPress_SO) target;

			if (GUILayout.Button("GenerateActionNames")) {  
				// call on button click
				taskKeyPressSO.GetAllInputActionNames();
			}
		}
	}
}