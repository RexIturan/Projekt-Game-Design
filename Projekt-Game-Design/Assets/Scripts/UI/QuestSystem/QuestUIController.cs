﻿using QuestSystem.ScriptabelObjects;
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

		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void Awake() {
			//register Action 
		}

		private void OnDisable() {
			//unregister Action
		}

		private void Start() {
			//get task panel form uiDocument
			taskContainer = uiDocument.rootVisualElement.Q<TaskContainer>();
			// taskContainer.SetVisibility(false);
		}

		private void Update() {
			if ( taskContainer != null ) {
				taskContainer.Quests = questContainer.activeQuests;
				taskContainer.UpdateComponent();
			}
			else {
				taskContainer = uiDocument.rootVisualElement.Q<TaskContainer>();
			}
		}
	}
}