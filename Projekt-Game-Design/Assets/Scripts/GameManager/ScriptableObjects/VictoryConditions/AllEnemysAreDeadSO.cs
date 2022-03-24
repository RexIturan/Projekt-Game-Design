using System.Linq;
using Combat;
using GDP01._Gameplay.Provider;
using UnityEngine;

namespace GameManager.ScriptableObjects.VictoryConditions {
	[CreateAssetMenu(fileName = "AllEnemysAreDeadSO", menuName = "GameManager/Conditions/Victory/AllEnemysAreDead")]
	public class AllEnemysAreDeadSO : GameEndConditionSO{
		public override bool CheckCondition() {
			//get char list

			CharacterList charlist = GameplayProvider.Current.CharacterList;

			bool allDead = true;
			if ( charlist.enemyContainer is { Count: > 0 } ) {
				allDead = !charlist.enemyContainer.Any(o => o.GetComponent<Targetable>().IsAlive);
			}

			return allDead;
		}
	}
}