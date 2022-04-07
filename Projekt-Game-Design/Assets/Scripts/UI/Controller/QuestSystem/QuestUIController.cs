using System;
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

		[SerializeField] private QuestContainerSO questContainer;
		
		// [SerializeField] private QuestSO currentQuest;
		private TaskContainer taskContainer;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void BindElements() {
			var root = uiDocument.rootVisualElement;
			//get task panel form uiDocument
			taskContainer = root.Q<TaskContainer>();
			// taskContainer.SetVisibility(false);
		}

		private void UnbindElements() {
			taskContainer = null;
		}

		private void UpdateQuestContainer() {
			taskContainer.Quests = questContainer.activeQuests;
			taskContainer.UpdateComponent();
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void Update() {
			if ( taskContainer != null ) {
				UpdateQuestContainer();
			}
			else {
				taskContainer = uiDocument.rootVisualElement.Q<TaskContainer>();
			}
		}

		private void OnEnable() {
			BindElements();
			Update();
			//todo update quest EC
		}

		private void OnDisable() {
			UnbindElements();
		}
	}
}