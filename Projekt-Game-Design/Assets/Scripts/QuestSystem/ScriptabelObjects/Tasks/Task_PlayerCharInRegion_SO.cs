using System.Collections.Generic;
using System.Linq;
using Characters.Types;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_PlayerCharInRegion_SO : TaskSO {

		[SerializeField] private Vector3Int minPos;
		[SerializeField] private Vector3Int maxPos;
		[SerializeField] private int numOfChars;
		[SerializeField] private bool updateAfterDone;

		private CharacterManager CharacterManager => GameplayProvider.Current.CharacterManager;
		private List<Vector3Int> triggerPositions;
		
///// Private Methodes /////////////////////////////////////////////////////////////////////////////

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

			var foundChars = CharacterManager.GetPlayerCharactersWhere(player =>
				triggerPositions.Any(pos => pos.Equals(player.GridPosition))).ToList();

			if ( foundChars.Count >= numOfChars ) {
				done = true;
			} else if ( updateAfterDone && foundChars.Count < numOfChars ) {
				done = false;
			}
			
			return done;
		}
		
///// Task SO //////////////////////////////////////////////////////////////////////////////////////

		public override TaskType Type => TaskType.PlayerCharInRegion;
		public override string BaseName => "CharInRegion";

		public override bool IsDone() {
			done = IsPlayerCharInRegion();
			return base.IsDone();
		}

		public override void StartTask() {
			base.StartTask();
			
			InitTriggerPositions();
			done = IsPlayerCharInRegion();
		}
	}
}