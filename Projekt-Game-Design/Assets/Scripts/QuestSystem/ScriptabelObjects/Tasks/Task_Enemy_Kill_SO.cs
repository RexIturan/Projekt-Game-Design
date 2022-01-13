using UnityEngine;
using Util.Types;

namespace QuestSystem.ScriptabelObjects {
	public class Task_Enemy_Kill_SO : TaskSO {

		//number
		[SerializeField] private int neededCount;
		//type
		[SerializeField] private EnemyTypeSO enemyType;

///// Private Variable
		
		private CharacterList characterList;
		private int currentCount;
		private int startCount;

///// Private Variable
		
///// TaskSO		
		
		public override TaskType Type { get; } = TaskType.Enemy_Kill;
		public override string BaseName { get; } = "EnemyKill";
		
		public override bool IsDone() {
			if ( active ) {
				currentCount = characterList.GetDeapEnemyCount(enemyType);
				if ( currentCount - startCount >= neededCount ) {
					done = true;
				}
			}
			
			return done;
		}

		public override void ResetTask() {
			base.ResetTask();
			currentCount = 0;
			startCount = 0;
		}

		public override void StartTask() {
			base.StartTask();
			characterList = CharacterList.FindInstant();
			startCount = characterList.GetDeapEnemyCount(enemyType);
		}

		public override TaskInfo GetInfo() {
			var info = base.GetInfo();
			info.showStatus = true;
			info.status = new RangedInt(0, neededCount, currentCount);
			return info;
		}
	}
}