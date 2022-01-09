using System.Collections.Generic;
using QuestSystem.ScriptabelObjects;
using UI.Components.QuestSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.QuestSystem {
	public class QuestUIController : MonoBehaviour {
///// Event Channels ///////////////////////////////////////////////////////////////////////////////
		
		//todo 
		// set Visibility
		// set Quest
		
///// Private Variables ////////////////////////////////////////////////////////////////////////////
		[SerializeField] private UIDocument uiDocument;

		[SerializeField] private QuestSO currentQuest;
		private TaskPanel taskPanel;

///// Private Functions ////////////////////////////////////////////////////////////////////////////
		
		private void SetInstructions(List<TaskSO> tasks) {
			taskPanel.instructionList.Clear();

			if ( tasks[0].type == TaskType.Composite ) {
				for ( int i = 1; i < tasks.Count; i++ ) {
					var task = tasks[i];
					var uiInstructionWrapper = new InstructionWrapper {
						state = task.done ? InstructionState.Done : InstructionState.Active,
						text = task.textTextBody.instructions, 
					};
					taskPanel.instructionList.Add(uiInstructionWrapper);
					//todo get instructiion from all sub Tasks
				}	
			}
			else {
				//todo get instructiion from singel Task
				var task = tasks[0];
				var uiInstructionWrapper = new InstructionWrapper {
					state = task.done ? InstructionState.Done : InstructionState.Active,
					text = task.textTextBody.instructions, 
				};
				taskPanel.instructionList.Add(uiInstructionWrapper);
			}
		}

		// Button Callbacks
		
		private void NextCallback() {
			Debug.Log("Next");

			if ( currentQuest != null && currentQuest.IsActive ) {
				currentQuest.Next();
			}
		}
		
		private void PreviousCallback() {
			Debug.Log("Previous");
			//TODO Implement
		}
		
		private void SkipCallback() {
			Debug.Log("Skip");
			//TODO Implement
		}
		
		private void HelpCallback() {
			Debug.Log("Help");
			//TODO Implement
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void Awake() {
			//register Action 
		}

		private void OnDisable() {
			//unregister Action
		}

		private void Start() {
			//get task panel form uiDocument
			taskPanel = uiDocument.rootVisualElement.Q<TaskPanel>();
			taskPanel.SetVisibility(false);
			
			taskPanel.SetNextCallback(NextCallback);
			taskPanel.SetPreviousCallback(PreviousCallback);
			taskPanel.SetSkipCallback(SkipCallback);
			taskPanel.SetHelpCallback(HelpCallback);
		}

		private void Update() {
			if ( currentQuest != null ) {
				taskPanel.SetVisibility(currentQuest.IsActive);
				SetTask(currentQuest);
			}
		}

		private void SetTask(QuestSO quest) {
			if(quest.CurrentTask == null)
				return;
			
			var task = quest.CurrentTask.task;
			
			taskPanel.Title = task.textTextBody.title;
			taskPanel.Description = task.textTextBody.description;
			
			List<TaskSO> tasks = new List<TaskSO> { task };

			if ( task is Task_Composite_SO compTask ) {
				foreach ( var subTask in compTask.subTasks ) {
					tasks.Add(subTask);
				}
			}
			SetInstructions(tasks);
			
			taskPanel.UpdateComponent();
		}
	}
}