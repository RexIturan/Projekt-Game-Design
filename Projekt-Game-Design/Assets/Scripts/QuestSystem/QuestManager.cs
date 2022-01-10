using System;
using System.Collections.Generic;
using Input;
using QuestSystem.ScriptabelObjects;
using UnityEngine;

namespace QuestSystem {
	public class QuestManager : MonoBehaviour {
		[SerializeField] private List<QuestSO> activeQuests;

		[SerializeField] private InputReader inputReader;
		
		private void OnEnable() {
			activeQuests.ForEach(quest => quest.Reset());
			activeQuests[0].Activate();
		}

		private void Update() {
			activeQuests.ForEach(quest => quest.UpdateQuestState());
			// activeQuests.RemoveAll(quest => quest.IsDone);
		}

		public void ActivateQuests() {
			foreach ( var quest in activeQuests ) {
				quest.Reset();
				quest.Activate();
			}
		}
	}
}