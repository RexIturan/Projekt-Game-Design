using System.Linq;
using GDP01._Gameplay.Provider;
using UnityEngine;

namespace GameManager.ScriptableObjects.GameOverConditions {
	[CreateAssetMenu(fileName = "BossDeadSO", menuName = "GameManager/Conditions/Victory/BossDeadSO")]
	public class BossDeadSO : GameEndConditionSO {

		[SerializeField] private EnemyTypeSO bossType;
		
		public override bool CheckCondition() {

			int bossesInLevel = GameplayProvider.Current.CharacterManager.GetEnemyCahracters()
				.FindAll(enemy => enemy.Type == bossType).Count;

			int deadBossesInLevel = GameplayProvider.Current.CharacterManager.GetEnemyCahracters()
				.FindAll(enemy => enemy.Type == bossType && enemy.IsDead).Count;
			
			return bossesInLevel > 0 && bossesInLevel == deadBossesInLevel;
		}
	}
}