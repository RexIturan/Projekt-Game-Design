using System.Collections.Generic;
using GDP01.Equipment;
using UnityEngine;

namespace Characters.Equipment.ScriptableObjects {
	[CreateAssetMenu(fileName = "EquipmentContainerSO", menuName = "Equipment/EquipmentContainerSO", order = 0)]
	public class EquipmentContainerSO : ScriptableObject {

		public List<EquipmentSheet> equipmentSheets = new List<EquipmentSheet>();

		public ItemSO GetItemFromEquipment(int playerID, EquipmentPosition equipmentPosition) {
			return equipmentSheets[playerID].GetEquipedItem(equipmentPosition);
		}

		public ItemSO SetItemInEquipment(int playerID, EquipmentPosition equipmentPosition, ItemSO item) {
			ItemSO previous = null;

			previous = equipmentSheets[playerID].GetEquipedItem(equipmentPosition);
			equipmentSheets[playerID].SetEquipedItem(equipmentPosition, item);

			return previous;
		}

		public ItemSO UnequipItemFor(int playerID, EquipmentPosition equipmentPosition) {
			return equipmentSheets[playerID].UnequipItem(equipmentPosition);
		}
	}
}