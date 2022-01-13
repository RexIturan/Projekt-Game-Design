using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_ReadText_SO : TaskSO {
		public override TaskType Type { get; } = TaskType.Read_Text;
		public override string BaseName { get; } = "ReadText";

		public override void StartTask() {
			base.StartTask();
			done = true;
		}
	}
}