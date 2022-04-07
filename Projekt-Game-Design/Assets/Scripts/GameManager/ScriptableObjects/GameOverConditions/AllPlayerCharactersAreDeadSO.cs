
using System.Linq;
using Combat;
using GDP01._Gameplay.Provider;
using UnityEngine;

namespace GameManager.ScriptableObjects.GameOverConditions {
	[CreateAssetMenu(fileName = "AllPlayerCharactersAreDeadSO", menuName = "GameManager/Conditions/GameOver/AllPlayerCharactersAreDead")]
	public class AllPlayerCharactersAreDeadSO : GameEndConditionSO{
		public override bool CheckCondition() {
			//get char list

			return GameplayProvider.Current.CharacterManager.GetPlayerCharacters()
				.Where(player => player.active)
				.All(player => player.IsDead);
		}
	}
}