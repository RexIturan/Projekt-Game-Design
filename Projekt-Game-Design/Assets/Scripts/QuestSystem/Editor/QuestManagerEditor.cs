using QuestSystem;
using UnityEditor;
using UnityEngine;

namespace GDP01.QuestSystem.Editor {
	[CustomEditor(typeof(QuestManager))]
	public class QuestManagerEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			var questManager = (QuestManager) target;
			
			base.OnInspectorGUI();
			
			if (GUILayout.Button("Activate Quests")) {  
				questManager.ActivateQuests();
			}
		}
	}
}