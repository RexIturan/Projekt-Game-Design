using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEditor;

namespace QuestSystem.ScriptabelObjects {
	[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/new Quest", order = 0)]
	public class QuestSO : ScriptableObject {
		public int questId;

		[TextArea][SerializeField] private string description;
		[SerializeField] private bool disabled;
		[SerializeField] private bool available;
		[SerializeField] private bool active;
		[SerializeField] private bool finished;
		[SerializeField] private bool repeatable;

		//prerequisits
		[SerializeField] private List<QuestSO> prerequisits;
		
		//tasks
		public int currentTaskIndex;
		[SerializeField] private int nextTaskIndex;
		[SerializeField] private List<Task_Wrapper> tasks = new List<Task_Wrapper>();

///// Private Functions ////////////////////////////////////////////////////////////////////////////

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public Task_Wrapper CurrentTask {
			get {
				if ( currentTaskIndex < tasks.Count ) {
					return tasks[currentTaskIndex];		
				}
				else {
					return null;
				}
			}
		}

		public bool IsDisabled => disabled;
		public bool IsAvailable => available;
		public bool IsDone => finished;
		public bool IsActive => active;

///// Public Functions /////////////////////////////////////////////////////////////////////////////

		public void Activate() {
			active = true;
			tasks[currentTaskIndex].task.StartTask();
		}

		public void Reset() {
			active = false;
			currentTaskIndex = 0;
			nextTaskIndex = 0;
			finished = false;
			foreach ( var wrapper in tasks ) {
				wrapper.task.ResetTask();
			}
		}

		public void UpdateQuestState() {
			if ( currentTaskIndex < tasks.Count ) {
				//todo idk if this is ok like this
				if ( tasks[currentTaskIndex].task.active ) {
					if ( tasks[currentTaskIndex].task.IsDone() ) {
						tasks[currentTaskIndex].task.StopTask();

						var next = 0; 
						if ( tasks[currentTaskIndex].task is Task_Composite_SO compTask ) {
							next += compTask.actualCount +1;
						}
						else {
							next++;
						}
				
						nextTaskIndex = currentTaskIndex + next;
					}	
				}
			}
			
			finished = true;
			for ( int i = 0; i < tasks.Count; i++ ) {
				if ( !tasks[i].task.IsDone() ) {
					finished = false;
				}
			}
		}

		public void UpdateAvailability() {
			available = HasPrerequisitesSatisfied() &&
			            !active && 
			            !IsDisabled &&
			            ( ( repeatable && finished ) || !finished );
		}
		
		public bool HasPrerequisitesSatisfied() {
			bool satisfied = true;
			for ( int i = 0; i < prerequisits.Count; i++ ) {
				if(prerequisits[i] == null)
					continue;
				
				if (!prerequisits[i].IsDone || prerequisits[i].IsActive ) {
					satisfied = false;
				}
			}
			return satisfied;
		}

		public void Next() {
			//todo test
			
			currentTaskIndex = nextTaskIndex;
			
			if ( currentTaskIndex < tasks.Count ) {
				tasks[currentTaskIndex].task.StartTask();	
			}

			if ( this.IsDone ) {
				this.active = false;
			}
		}
		
///// Editor Functions /////////////////////////////////////////////////////////////////////////////		
		
// #define UNITY_EDITOR

		#region Editor Only
#if UNITY_EDITOR
		
		private void OnValidate() {
			foreach ( var taskWrapper in tasks ) {
				if ( taskWrapper.UpdateTaskType(this) ) {
					RenameTasks();
				}
			}
				
			RemoveUnusedTasks();
			if ( ReorderTasks() ) {
				RenameTasks();
			}
			
			UpdateCompositTasks();
			
			// var questPath = AssetDatabase.GetAssetPath(this);
			// var objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(questPath);
			
			// for ( int i = 0; i < testTasks.Count; i++ ) {
			// 	var taskPath = AssetDatabase.GetAssetPath(testTasks[i].task);
			// 	var guid = AssetDatabase.GUIDFromAssetPath(taskPath);
			// 	
			// }

			// UpdateQuestState();
		}

		public void UpdateCompositTasks() {
			for ( var i = 0; i < tasks.Count; i++ ) {
				var wrapper = tasks[i];
				if ( wrapper.task is Task_Composite_SO compTask ) {
					var subTaskLen = i + compTask.subTasksCount;
					var remainingTasks = tasks.Count -1 - i;
					var actualSubTaskLen = Mathf.Min(subTaskLen, remainingTasks);
					List<TaskSO> subTasks = new List<TaskSO>(); 
					for ( int j = i+1; j <= i+actualSubTaskLen; j++ ) {
						subTasks.Add(tasks[j].task);
						//todo sub tasks von subtasks
					}
					compTask.SetSubTasks(subTasks);
				}
			}
		}
		
		public bool ReorderTasks() {
			var questPath = AssetDatabase.GetAssetPath(this);
			var objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(questPath);

			bool shouldReorder = false;

			int objIdx = 0;
			foreach ( var t in tasks ) {
				if ( t.task != null ) {

					if(objIdx >= objects.Length)
						break;

					var task = objects[objIdx] as TaskSO;

					if ( t.task != task) {
						shouldReorder = true;
					}
					objIdx++;
				}
			}

			if ( shouldReorder ) {
				// Debug.Log("reorder");
				
				foreach ( var obj in objects ) {
					if ( obj is TaskSO ) {
						AssetDatabase.RemoveObjectFromAsset(obj);	
					}
				}
				// AssetDatabase.SaveAssets();
				
				foreach ( var taskWrapper in tasks ) {
					if ( taskWrapper.task != null ) {
						AssetDatabase.AddObjectToAsset(taskWrapper.task, this);
					}
				}
				AssetDatabase.SaveAssets();
			}
			else {
				// Debug.Log("dont reorder");
			}

			return shouldReorder;
		}

		public void RemoveUnusedTasks() {
			var path = AssetDatabase.GetAssetPath(this);
			var objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

			bool removed = false;
			
			foreach ( var obj in objects ) {
				if ( obj is TaskSO ) {
					if ( tasks.All(wrapper => wrapper.task != obj) ) {
						removed = true;
						AssetDatabase.RemoveObjectFromAsset(obj);
					}	
				}
			}

			if ( removed ) {
				AssetDatabase.SaveAssets();
			}
		}

		public class TaskCounter {
			public int current;
			public int max;
		}
		
		public void RenameTasks() {
			bool renamed = false;
			var prefix = "";
			
			Stack<TaskCounter> subTaskCounter = new Stack<TaskCounter>();

			for ( int i = 0; i < tasks.Count; i++ ) {
				var wrapper = tasks[i];
				var task = tasks[i].task; 
				var type = tasks[i].type;
				
				if ( task != null ) {
					prefix = i + "_";

					if ( subTaskCounter.Count > 0 ) {
						foreach ( var taskCounter in subTaskCounter ) {
							prefix += taskCounter.current + "_";
						}
						
						var counter = subTaskCounter.Peek(); 
						counter.current++;
					}
					
					var newName = prefix + task.BaseName + "_" + wrapper.name;
					
					if ( task.name != newName ) {
						task.name = newName;
						renamed = true;
					}

					if ( type == TaskType.Composite ) {
						if ( task is Task_Composite_SO compTask ) {
							if ( compTask.subTasksCount > 0 ) {
								subTaskCounter.Push(new TaskCounter {
									current = 0,
									max = compTask.subTasksCount
								});	
							}
						}
					}

					if ( subTaskCounter.Count > 0 ) {
						while (subTaskCounter.Count > 0 && subTaskCounter.Peek().max == subTaskCounter.Peek().current ) {
							subTaskCounter.Pop();
						}
					}
				}
			}

			if ( renamed ) {
				AssetDatabase.SaveAssets();
			}
		}
		
		public TaskSO CreateNewTask(TaskType type) {
			Debug.Log($"{type}");

			TaskSO task = null;
			
			task = ScriptableObject.CreateInstance<Task_KeyPress_SO>();
			task.name = "Task_KeyPress_SO";
				
			AssetDatabase.AddObjectToAsset(task, this);
			AssetDatabase.SaveAssets();

			return task;
		}

		public void AddNewTask() {
			// if(tasks == null){
			// 	InitTasks();
			// }

			TaskSO task = null;
			task = ScriptableObject.CreateInstance<Task_KeyPress_SO>();
			task.name = "Task_KeyPress_SO" + tasks.Count;
			tasks.Add(new Task_Wrapper {
				type = TaskType.Key_Press,
				name = "new Task",
				task = task,
			});
			
			AssetDatabase.AddObjectToAsset(task, this);
			AssetDatabase.SaveAssets();
		}
#endif
		#endregion
	}
}