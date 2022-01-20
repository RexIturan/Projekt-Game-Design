using Characters.Equipment.ScriptableObjects;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_ItemEquipped_SO : TaskSO {
		public override TaskType Type { get; } = TaskType.Item_Equipped;
		public override string BaseName { get; } = "ItemEquipped";
		
		[SerializeField] private ItemSO item;
		[SerializeField] private EquipmentContainerSO equipmentContainer;
		
		public override bool IsDone() {
			if ( active ) {
				var equipmentInventories = equipmentContainer.equipmentSheets;
				done = false;
				foreach ( var equipInv in equipmentInventories ) {
					var equipArray = equipInv.EquipmentToArray();
					foreach ( var equipItem in equipArray ) {
						if ( item != null ) {
							if ( equipItem == item ) {
								done = true;
							}
						}
						else {
							done = true;
						}
					}
				}
			}

			return done;
		}
	}
}
