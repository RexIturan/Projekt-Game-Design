using System;
using UnityEditor;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	[Serializable]
	public struct TaskTextBody {
		public string title;
		[TextArea]public string description;
		[TextArea]public string instructions;
		// [TextArea]public string hint;
	}

	public enum TaskType {
		Key_Press,
		Read_Text,
		Event_Raised,
		Item_Equipping,
		Item_Collection,
		Object_Selected,
		Char_At_Pos,
		Enemy_Dead,
		Round_Timer,
		Composite
	}
	
	[Serializable]
	public class Task_Wrapper {
		public string name;
		public TaskType type;
		public TaskSO task;

#if UNITY_EDITOR
		public bool UpdateTaskType(QuestSO quest) {

			if ( task != null ) {
				if (task.type == type) {
					return false;	
				}
			}

			// var description = task.textTextBody;
			
			var path = AssetDatabase.GetAssetPath(quest);
			
			if ( task != null ) {
				AssetDatabase.RemoveObjectFromAsset(task);
				ScriptableObject.DestroyImmediate(task);
			}
			
			switch ( type ) {
				case TaskType.Key_Press:
					task = ScriptableObject.CreateInstance<Task_KeyPress_SO>();
					
					break;
				case TaskType.Composite:
					task = ScriptableObject.CreateInstance<Task_Composite_SO>();
					break;
				
				case TaskType.Read_Text:
				default:
					task = ScriptableObject.CreateInstance<Task_ReadText_SO>();
					break;
			}

			// task.textTextBody = description;
			
			AssetDatabase.AddObjectToAsset(task, path);
			AssetDatabase.SaveAssets();
			return true;
		}
#endif
		
		//ui fix
		[SerializeField] private bool showProperty;
		[SerializeField] private string button;
		[SerializeField] private float drawerHeight;
	}
	
	public abstract class TaskSO : ScriptableObject {
		protected readonly string baseName = "Task_SO";

		public bool active;
		public bool done;
		public TaskType type;
		public TaskTextBody textTextBody;
		
		//todo public -> [SerializeField] protected
		// public TaskCondition condition;

		private void Awake() {
			type = Type;
		}

		protected abstract TaskType Type { get; }
		public abstract string BaseName { get; }
		
		public abstract bool IsDone();
		public abstract void ResetTask();
		public abstract void StartTask();
		public abstract void StopTask();
	}
}