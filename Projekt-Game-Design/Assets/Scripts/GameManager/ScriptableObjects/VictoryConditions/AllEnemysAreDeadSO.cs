using System.Linq;
using Combat;
using GDP01._Gameplay.Provider;
using UnityEngine;

namespace GameManager.ScriptableObjects.VictoryConditions {
	[CreateAssetMenu(fileName = "AllEnemysAreDeadSO", menuName = "GameManager/Conditions/Victory/AllEnemysAreDead")]
	public class AllEnemysAreDeadSO : GameEndConditionSO{
		public override bool CheckCondition() {
			return GameplayProvider.Current.CharacterManager.GetEnemyCahracters()
				.All(enemy => enemy.IsDead);
		}
	}
}