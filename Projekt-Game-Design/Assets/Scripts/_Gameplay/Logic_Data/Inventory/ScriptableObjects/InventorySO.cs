using System.Collections.Generic;
using System.Linq;
using GDP01.Equipment;
using SaveSystem.V2.Data;
using UnityEngine;

/// <summary>
/// InventorySO
/// All items a player has (all equipped and all not equipped items in possession)
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject, ISaveState<InventorySO.InventoryData> {
	public struct InventoryData {
		public int size;
		public ItemSO.ItemTypeData[] inventory;
	}

	[SerializeField] private ItemSO[] inventorySlots;
	public ItemSO[] InventorySlots => inventorySlots;
	
///// Save State ///////////////////////////////////////////////////////////////////////////////////
	
	public InventoryData Save() {
		return new InventoryData {
			size = inventorySlots.Length,
			inventory = inventorySlots.Select(item => item?.ToData()).ToArray(),
		};
	}

	public void Load(InventoryData data) {
		inventorySlots = new ItemSO[data.size];
		if ( data.inventory != null ) {
			for ( int i = 0; i < data.size; i++ ) {
				inventorySlots[i] = data.inventory[i]?.obj;
			}
		}
	}

	public void Claer(int size) {
		inventorySlots = new ItemSO[size];
	}

	public void AddItemAt(int index, ItemSO item) {
		inventorySlots[index] = item;
	}

	public bool AddItem(ItemSO item) {
		var firstFreeSpace = -1;
		for ( int i = 0; i < inventorySlots.Length; i++ ) {
			if ( inventorySlots[i] == null ) {
				firstFreeSpace = i;
				break;
			}
		}

		if ( firstFreeSpace >= 0 ) {
			inventorySlots[firstFreeSpace] = item;
			return true;
		}
		else {
			return false;
		}
	}

	public bool Contains(ItemSO item) {
		return inventorySlots.Contains(item);
	}
	
	public ItemSO RemoveItemAt(int slotId) {
		var item = inventorySlots[slotId];
		inventorySlots[slotId] = null;
		return item;
	}
}
