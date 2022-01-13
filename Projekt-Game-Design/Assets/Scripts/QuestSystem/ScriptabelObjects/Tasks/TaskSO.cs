using System;
using UnityEditor;
using UnityEngine;
using Util.Types;

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
		Ability_Selected,
		Item_In_Inventory,
		Item_Equipped,
		Composite,
		Switch_Active,
		Enemy_Kill,
		Enemy_Dead,
		// Event_Raised,
		// Object_Selected,
		// Char_At_Pos,
		// Enemy_Dead,
		// Round_Timer,
	}
	
	[Serializable]
	public class Task_Wrapper {
		public string name;
		public TaskType type;
		public TaskSO task;

// #define UNITY_EDITOR
#if UNITY_EDITOR
		public bool UpdateTaskType(QuestSO quest) {

			if ( task != null ) {
				if (task.Type == type) {
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
				
				case TaskType.Ability_Selected:
					task = ScriptableObject.CreateInstance<Task_AbilitySelected_SO>();
					break;
				
				case TaskType.Item_In_Inventory:
					task = ScriptableObject.CreateInstance<Task_ItemInInventory_SO>();
					break;

				case TaskType.Item_Equipped:
					task = ScriptableObject.CreateInstance<Task_ItemEquipped_SO>();
					break;

				case TaskType.Switch_Active:
					task = ScriptableObject.CreateInstance<Task_SwitchActive_SO>();
					break;

				case TaskType.Enemy_Kill:
					task = ScriptableObject.CreateInstance<Task_Enemy_Kill_SO>();
					break;
				
				case TaskType.Enemy_Dead:
					task = ScriptableObject.CreateInstance<Task_Enemy_Dead_SO>();
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
	
	public struct TaskInfo {
		public bool active;
		public bool done;
		public bool failed;
		public bool showStatus;
		public string text;
		public RangedInt status;
	}
	
	public abstract class TaskSO : ScriptableObject {
		protected readonly string baseName = "Task_SO";

		public bool active;
		public bool done;
		public TaskTextBody textTextBody;
		
		//todo public -> [SerializeField] protected
		// public TaskCondition condition;

		public abstract TaskType Type { get; }
		public abstract string BaseName { get; }

		public virtual bool IsDone() {
			return done;
		}

		public virtual void ResetTask() {
			done = false;
			active = false;
		}

		public virtual void StartTask() {
			Debug.Log($"Start Task {this.name}");
			active = true;
		}

		public virtual void StopTask() {
			active = false;
		}
		
		public virtual TaskInfo GetInfo() {
			return new TaskInfo {
				done = this.done,
				failed = false,
				active = this.active,
				text = textTextBody.instructions,
				showStatus = false,
				status = new RangedInt(0, 1, this.done ? 1: 0)
			};
		}
	}
}