﻿using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_ItemInInventory_SO : TaskSO {
		public override TaskType Type { get; } = TaskType.Item_In_Inventory;
		public override string BaseName { get; } = "ItemInInventory";

		[SerializeField] private ItemTypeSO itemType;
		[SerializeField] private InventorySO inventory;
		
		public override bool IsDone() {
			if ( active ) {
				var playerInventory = inventory.InventorySlots;
				done = false;
				foreach ( var invItem in playerInventory ) {
					if ( itemType != null ) {
						if ( invItem == itemType ) {
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
	}
}