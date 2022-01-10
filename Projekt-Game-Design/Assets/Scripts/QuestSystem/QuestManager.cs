using QuestSystem.ScriptabelObjects;
using UnityEngine;

namespace QuestSystem {
	public class QuestManager : MonoBehaviour {
		[SerializeField] private QuestContainerSO questContainer;
		
		private void OnEnable() {
			questContainer.ResetQuests();
		}

		private void Update() {
			questContainer.UpdateQuests();
		}

		public void ActivateQuests() {
			questContainer.ResetQuests();
			questContainer.UpdateQuests();
		}
	}
}