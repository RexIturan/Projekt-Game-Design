using System.Collections.Generic;
using QuestSystem.ScriptabelObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.QuestSystem {
	public class TaskContainer : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseComponentName = "TaskContainer";

		private static readonly string baseUssClassName = "taskContainer";
		
		private static readonly string containerSuffix = "container";
		private static readonly string taskPanelContainerSuffix = "taskPanelContainer";
		
		private static readonly string defaultStyleSheet = "UI/taskContainer";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////



///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement container;
		private VisualElement taskPanelContainer;
		
		private List<TaskPanel> taskPanels;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

		public List<QuestSO> Quests { get; set; }

///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////

		private void InitContainer( ref VisualElement container, string ComponentName, string[] suffix) {
			container = new VisualElement {
				name = GetComponentName(ComponentName),
			};
			foreach ( var s in suffix ) {
				container.AddToClassList(GetClassNameWithSuffix(s));
			}
		}

		private void InitLabel( ref Label label, string ComponentName, string labelText, string suffix) {
			label = new Label {
				name = GetComponentName(ComponentName),
				text = labelText
			};
			label.AddToClassList(GetClassNameWithSuffix(suffix));
		}

		private void BuildComponent() {
			this.name = "TaskPanel";

			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			this.AddToClassList(baseUssClassName);

			InitContainer(
				ref container, 
				"Container", 
				new []{containerSuffix});
			
			
			InitContainer(
				ref taskPanelContainer, 
				"TaskPanelContainer", 
				new []{taskPanelContainerSuffix});
			
			container.Add(taskPanelContainer);
			
			this.Add(container);
		}
		
		private void SetTask(TaskPanel taskPanel, QuestSO quest) {
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
			SetInstructions(taskPanel, tasks);
			
			taskPanel.UpdateComponent();
		}
		
		private void SetInstructions(TaskPanel taskPanel, List<TaskSO> tasks) {
			taskPanel.instructionList.Clear();

			if ( tasks[0].Type == TaskType.Composite ) {
				for ( int i = 1; i < tasks.Count; i++ ) {
					TryAddInstructionWrapper(taskPanel, tasks, i);
				}	
			}
			else {
				TryAddInstructionWrapper(taskPanel, tasks, 0);
			}
		}

		private void TryAddInstructionWrapper(TaskPanel taskPanel, List<TaskSO> tasks, int index) {
			if(tasks[index] == null || tasks[index].Type == TaskType.Read_Text)
				return;
			
			taskPanel.instructionList.Add(tasks[index].GetInfo());
		}
		
///// Button Callbacks /////////////////////////////////////////////////////////////////////////////
		
		private void NextCallback(int index) {
			var quest = Quests[index];
			if ( quest != null && quest.IsActive ) {
				quest.Next();
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
		
///// Util /////////////////////////////////////////////////////////////////////////////////////////
		
		private string GetComponentName(string component) {
			return $"{baseComponentName}-{component}";
		}
		
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}

///// PUBLIC FUNCTIONS  ////////////////////////////////////////////////////////////////////////////
	
		public void UpdateComponent() {

			if ( taskPanels.Count != Quests.Count ) {
				taskPanelContainer.Clear();
				taskPanels.Clear();
				for ( int i = 0; i < Quests.Count; i++ ) {
					var taskPanel = new TaskPanel();
					//todo does this work?
					//todo potential problem, infinite callbacks
					var index = i;
					taskPanel.SetNextCallback(() => NextCallback(index));	
					taskPanels.Add(taskPanel);
					taskPanelContainer.Add(taskPanel);
				}
			}
			
			for ( int i = 0; i < Quests.Count; i++ ) {

				var quest = Quests[i];

				//todo set false?
				taskPanels[i].SetVisibility(true);
				if ( quest is { } ) {
					taskPanels[i].SetVisibility(quest.IsActive);
					SetTask(taskPanels[i], quest);
				}
			}
		}
		
		public void SetVisibility(bool visibility) {
			if ( visibility ) {
				this.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
			} else {
				this.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////
		public new class UxmlFactory : UxmlFactory<TaskContainer, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is TaskContainer element ) {
					element.Clear();

					element.BuildComponent();
					element.UpdateComponent();
				}
			}
		}
		
		public TaskContainer() : this("TaskContainer"){}

		public TaskContainer(string title) {
			Quests = new List<QuestSO>();
			taskPanels = new List<TaskPanel>();
			
			Quests.Add(null);
			Quests.Add(null);
			
			BuildComponent();
			UpdateComponent();
		}
	}
}