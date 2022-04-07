using System.Collections.Generic;
using System.Linq;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using UnityEngine;

namespace GameManager.ScriptableObjects.VictoryConditions {
	[CreateAssetMenu(fileName = "CharInRangeSO", menuName = "GameManager/Conditions/Victory/CharInRangeSO")]
	public class CharInRangeSO : GameEndConditionSO {

		[SerializeField] private bool done = false;
		[SerializeField] private Vector3Int minPos;
		[SerializeField] private Vector3Int maxPos;
		[SerializeField] private int numOfChars;
		[SerializeField] private bool updateAfterDone;

		private CharacterManager CharacterManager => GameplayProvider.Current.CharacterManager;
		private List<Vector3Int> triggerPositions;
		
		private void InitTriggerPositions() {
			triggerPositions = new List<Vector3Int>();
			for ( int z = minPos.z; z <= maxPos.z; z++ ) {
				for ( int y = minPos.y; y <= maxPos.y; y++ ) {
					for ( int x = minPos.x; x <= maxPos.x; x++ ) {
						triggerPositions.Add(new Vector3Int(x,y,z));
					}	
				}
			}
		}

		private bool IsPlayerCharInRegion() {

			var foundChars = CharacterManager?.GetPlayerCharactersWhere(player =>
				triggerPositions.Any(pos => pos.Equals(player.GridPosition))).ToList();

			if ( foundChars is {} && foundChars.Count >= numOfChars ) {
				done = true;
			} else if ( updateAfterDone && foundChars.Count < numOfChars ) {
				done = false;
			}
			
			return done;
		}
		
		public override bool CheckCondition() {
			return IsPlayerCharInRegion();
		}

		public void Init() {
			InitTriggerPositions();
		}
		
		public override void Reset() {
			done = false;
			Init();
		}
	}
}