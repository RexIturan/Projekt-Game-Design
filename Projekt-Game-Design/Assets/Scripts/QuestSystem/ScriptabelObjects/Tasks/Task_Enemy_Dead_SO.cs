using System;
using UnityEngine;
using Util.Types;

namespace QuestSystem.ScriptabelObjects {
	public class Task_Enemy_Dead_SO : TaskSO {

		//number
		[SerializeField] private int neededCount;
		//type
		[SerializeField] private EnemyTypeSO enemyType;

///// Private Variable
		
		private int currentCount;
		private CharacterList characterList;

///// TaskSO	
		
		public override TaskType Type { get; } = TaskType.Enemy_Dead;
		public override string BaseName { get; } = "EnemyDead";

		public override bool IsDone() {
			if ( active ) {
				if ( characterList is { } ) {
					done = false;

					currentCount = 0;
					currentCount = characterList.GetDeapEnemyCount(enemyType);
					
					if ( currentCount >= neededCount ) {
						done = true;
					}
				}
			}
			
			return done;
		}

		public override void ResetTask() {
			base.ResetTask();
			currentCount = 0;
		}

		public override void StartTask() {
			base.StartTask();
			characterList = CharacterList.FindInstant();
		}

		public override void StopTask() {
			base.StopTask();
		}

		public override TaskInfo GetInfo() {
			var info = base.GetInfo();
			info.showStatus = true;
			info.status = new RangedInt(0, neededCount, currentCount);
			return info;
		}
	}
}