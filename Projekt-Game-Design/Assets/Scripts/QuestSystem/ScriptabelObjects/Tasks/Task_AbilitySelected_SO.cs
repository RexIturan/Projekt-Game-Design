using GDP01.Characters.Component;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_AbilitySelected_SO : TaskSO {
		
		[SerializeField] private AbilitySO ability;
		
		private CharacterList characterList;
		
		public override TaskType Type { get; } = TaskType.Ability_Selected;
		public override string BaseName { get; } = "AbilitySelected";

		public override bool IsDone() {
			if ( active ) {
				done = false;
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
			}
			
			return done;
		}

		public override void StartTask() {
			base.StartTask();
			characterList = CharacterList.FindInstant();
		}
	}
}