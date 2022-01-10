using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_ReadText_SO : TaskSO {
		protected override TaskType Type { get; } = TaskType.Read_Text;
		public override string BaseName { get; } = "ReadText";

		public override bool IsDone() {
			return done;
		}

		public override void ResetTask() {
			done = false;
			active = false;
		}

		public override void StartTask() {
			Debug.Log($"Start Task {this.name}");
			active = true;
			done = true;
		}
		
		public override void StopTask() {
			active = false;
		}
	}
}