using UnityEditor;
using UnityEngine;

namespace QuestSystem.Editor {
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