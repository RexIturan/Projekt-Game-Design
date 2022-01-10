using Characters.Ability;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_AbilitySelected_SO : TaskSO {
		
		protected override TaskType Type { get; } = TaskType.Ability_Selected;
		public override string BaseName { get; } = "AbilitySelected";

		[SerializeField] private AbilitySO ability;
		
		private CharacterList characterList;
		
		public override bool IsDone() {

			if ( characterList != null ) {
				var players = characterList.playerContainer;
				foreach ( var player in players ) {
					var abilityController = player.GetComponent<AbilityController>();
					
					if ( abilityController.abilitySelected ) {
						done = true;
						
						if ( ability != null ) {
							if ( abilityController.SelectedAbilityID != ability.id) {
								done = false;
							}	
						}
					}
				}
			}
			else {
				characterList = CharacterList.FindInstant();
			}
			
			return done;
		}

		public override void ResetTask() {
			done = false;
			active = false;
		}

		public override void StartTask() {
			Debug.Log($"Start Task {this.name}");
			characterList = CharacterList.FindInstant();
			active = true;
		}

		public override void StopTask() {
			active = false;
		}
	}
}