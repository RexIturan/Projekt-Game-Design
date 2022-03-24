
using System.Linq;
using Combat;
using GDP01._Gameplay.Provider;
using UnityEngine;

namespace GameManager.ScriptableObjects.GameOverConditions {
	[CreateAssetMenu(fileName = "AllPlayerCharactersAreDeadSO", menuName = "GameManager/Conditions/GameOver/AllPlayerCharactersAreDead")]
	public class AllPlayerCharactersAreDeadSO : GameEndConditionSO{
		public override bool CheckCondition() {
			//get char list

			CharacterList charlist = GameplayProvider.Current.CharacterList;

			bool allDead = true;
			if ( charlist.playerContainer is { Count: > 0 } ) {
				allDead = !charlist.playerContainer.Any(o => o.GetComponent<Targetable>().IsAlive);
			}

			return allDead;
		}
	}
}