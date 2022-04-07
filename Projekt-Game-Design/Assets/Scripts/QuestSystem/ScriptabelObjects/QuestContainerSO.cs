using System;
using SaveSystem.SaveFormats;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	[CreateAssetMenu(fileName = "questContainer", menuName = "Quest/Quest Container", order = 0)]
	public class QuestContainerSO : ScriptableObject {

		public List<QuestSO> allQuests = new List<QuestSO>();
		public List<QuestSO> activeQuests = new List<QuestSO>();

		
		//todo move to savesystem?
		public void Initialise(List<Quest_Save> quests) {
			ResetQuests();
			foreach(Quest_Save questSave in quests) {
				QuestSO quest = allQuests[questSave.questId];

				quest.Disabled = questSave.disabled;
				quest.overrideTaskIndex = questSave.currentTaskIndex;
			}

			foreach ( Quest_Save questSave in quests ) {
				if ( questSave.active && !questSave.disabled ) {
					QuestSO quest = allQuests[questSave.questId];
					quest.FulfillPrerequisites();
					// activeQuests.Add(quest);
				}
			}
		}

		public void ResetQuests() {
			activeQuests.Clear();
			
			foreach ( var quest in allQuests ) {
				quest.Reset();
			}
		}

		public void UpdateQuests() {
			foreach ( var quest in allQuests ) {

				quest.UpdateAvailability();
				
				if ( !activeQuests.Contains(quest) ) {
					if ( quest.IsAvailable ) {
						activeQuests.Add(quest);
						quest.Activate();
					}
				} else {
					quest.UpdateQuestState();
					if ( !quest.IsActive ) {
						activeQuests.Remove(quest);
					}
				}
			}
		}

		#if UNITY_EDITOR
		private void OnValidate() {
			for ( int i = 0; i < allQuests.Count; i++ ) {
				var q = allQuests[i];
				if ( q is { } ) {
					q.questId = i;
				}
			}
		}
		#endif
		
	}
}