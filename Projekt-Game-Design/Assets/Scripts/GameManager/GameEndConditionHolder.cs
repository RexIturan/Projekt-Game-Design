using System;
using System.Collections.Generic;
using GameManager.ScriptableObjects;
using UnityEngine;

namespace GameManager {
	public class GameEndConditionHolder : MonoBehaviour {
		[Header("Victory Conditions"), SerializeField] private List<GameEndConditionSO> victoryConditions;
		[Header("GameOver Conditions"), SerializeField] private List<GameEndConditionSO> gameOverConditions;


		private void OnEnable() {
			Reset();
		}

		private void Reset() {
			victoryConditions.ForEach(condition => condition.Reset());
			gameOverConditions.ForEach(condition => condition.Reset());
		}
		
		public bool[] CheckGameEndingConditions() {
			bool[] conditionResult = new bool[] {false, false};
			
			//for each gameover condition -> check ->
			// -> set gameOver = true
			foreach ( var condition in gameOverConditions ) {
				if ( condition.CheckCondition() ) {
					conditionResult[1] = true;
				}
			}
			
			// for each victory condition -> check
			// -> set victory = true
			foreach ( var condition in victoryConditions ) {
				if ( condition.CheckCondition() ) {
					conditionResult[0] = true;
				}
			}

			return conditionResult;
		}
	}
}