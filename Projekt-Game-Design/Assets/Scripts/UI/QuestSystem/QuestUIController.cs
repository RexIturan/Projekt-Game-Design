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

		[SerializeField] private List<QuestSO> activeQuests;
		
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
			taskContainer.Quests = activeQuests;
			taskContainer.UpdateComponent();
		}
	}
}