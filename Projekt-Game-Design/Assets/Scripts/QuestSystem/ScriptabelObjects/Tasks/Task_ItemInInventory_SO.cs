using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_ItemInInventory_SO : TaskSO {
		public override TaskType Type { get; } = TaskType.Item_In_Inventory;
		public override string BaseName { get; } = "ItemInInventory";

		[SerializeField] private ItemSO item;
		[SerializeField] private InventorySO inventory;
		
		public override bool IsDone() {
			if ( active ) {
				var playerInventory = inventory.playerInventory;
				done = false;
				foreach ( var invItem in playerInventory ) {
					if ( item != null ) {
						if ( invItem == item ) {
							done = true;
						}
					}
					else {
						done = true;
					}
				}
			}

			return done;
		}

		public override void ResetTask() {
			done = false;
			active = false;
		}

		public override void StartTask() {
			Debug.Log($"Start Task {this.name}");
			active = true;
		}

		public override void StopTask() {
			active = false;
		}
	}
}