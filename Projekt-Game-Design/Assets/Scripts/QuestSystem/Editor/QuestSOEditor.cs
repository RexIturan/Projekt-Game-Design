using QuestSystem.ScriptabelObjects;
using UnityEditor;
using UnityEngine;

namespace GDP01.QuestSystem.Editor {
	[CustomEditor(typeof(QuestSO))]
	public class QuestSOEditor : UnityEditor.Editor {
		
		private SerializedObject so;
		
		private void OnEnable() {  
			so = serializedObject;
		}
		
		public override void OnInspectorGUI() {
			so.Update();

			var quest = (QuestSO) target;
			
			base.OnInspectorGUI();

			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Add New Task")) {  
				quest.AddNewTask();
			}
			
			if (GUILayout.Button("Update Names")) {  
				quest.ReorderTasks();
				quest.RenameTasks();
				quest.UpdateCompositTasks();
			}
			
			EditorGUILayout.EndHorizontal();
			
			if (so.ApplyModifiedProperties()) {
				// true if something changed
				
				so.Update();
			}
		}

		// private static void SetupTaskList(ReorderableList reorderableList) {
		// 	var lineHight = EditorGUIUtility.singleLineHeight;
		// 	var marginTop = 5;
		// 	var marginLeft = 5;
		// 	
		// 	reorderableList.draggable = true;
		// 	reorderableList.elementHeight *= 2f;
		// 	reorderableList.drawHeaderCallback += rect => GUI.Label(rect, "Tasks");
		// 	
		// 	var originHeight = reorderableList.elementHeight;
		// 	reorderableList.drawElementCallback +=
		// 		(Rect rect, int index, bool isActive, bool isFocused) => {
		// 			var r = rect;
		// 			r.height = lineHight;
		// 			r.y += marginTop;
		// 			r.x += marginLeft;
		// 			
		// 			var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
		//
		//
		// 			var name = prop.FindPropertyRelative("name");
		// 			var type = prop.FindPropertyRelative("type");
		// 			var attribute = prop.FindPropertyRelative("attribute");
		// 			var task = attribute.FindPropertyRelative("task");
		// 			
		// 			r.width = 100;
		// 			EditorGUI.PropertyField(r, name, GUIContent.none);
		// 			r.x += 100;
		// 			EditorGUI.PropertyField(r, type, GUIContent.none);
		// 			r.x += 125;
		//
		// 			r.x = rect.x + marginLeft;
		// 			r.width = rect.width;
		// 			r.y += lineHight + marginTop;
		// 			if (task.objectReferenceValue != null) {
		// 				// EditorGUI.ObjectField(r, task, GUIContent.none);
		// 				EditorGUI.PropertyField(r, task, GUIContent.none);
		// 			}
		// 			else
		// 				EditorGUI.PropertyField(r, task, GUIContent.none);
		// 		};
		//
		// 	reorderableList.elementHeightCallback += index => {
		// 		var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
		// 		var attribute = prop.FindPropertyRelative("attribute");
		// 		var task = attribute.FindPropertyRelative("task");
		// 		var elementHeight = EditorGUI.GetPropertyHeight(task, true);
		// 		return task.objectReferenceValue != null ? 
		// 			reorderableList.elementHeight + elementHeight + marginTop - lineHight : 
		// 			reorderableList.elementHeight;
		// 	}; 
		// 	
		// 	reorderableList.onChangedCallback += list => list.serializedProperty.serializedObject.ApplyModifiedProperties();
		// }
	}
}