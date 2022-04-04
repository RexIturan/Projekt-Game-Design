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
	public static readonly int SIZE = 28;

	public struct InventoryData {
		public ItemTypeSO.ItemTypeData[] inventory;
	}

	//todo is List better here?
	[SerializeField] private ItemTypeSO[] inventorySlots;
	public ItemTypeSO[] InventorySlots => inventorySlots;
	
///// Save State ///////////////////////////////////////////////////////////////////////////////////
	
	public InventoryData Save() {
		return new InventoryData {
			inventory = inventorySlots.Select(item => item?.ToData()).ToArray(),
		};
	}

	public void Load(InventoryData data) {
		inventorySlots = new ItemTypeSO[SIZE];
		if ( data.inventory != null ) {
			for ( int i = 0; i < SIZE; i++ ) {
				inventorySlots[i] = data.inventory[i]?.obj;
			}
		}
	}

	public void Claer(int size) {
		inventorySlots = new ItemTypeSO[SIZE];
	}

	public void AddItemAt(int index, ItemTypeSO itemType) {
		inventorySlots[index] = itemType;
	}

	public bool AddItem(ItemTypeSO itemType) {
		var firstFreeSpace = -1;
		for ( int i = 0; i < inventorySlots.Length; i++ ) {
			if ( inventorySlots[i] == null ) {
				firstFreeSpace = i;
				break;
			}
		}

		if ( firstFreeSpace >= 0 ) {
			inventorySlots[firstFreeSpace] = itemType;
			return true;
		}
		else {
			return false;
		}
	}

	public bool Contains(ItemTypeSO itemType) {
		return inventorySlots.Contains(itemType);
	}
	
	public ItemTypeSO RemoveItemAt(int slotId) {
		var item = inventorySlots[slotId];
		inventorySlots[slotId] = null;
		return item;
	}

	public bool IsSlotIdValid(int id) {
		return id >= 0 && id < inventorySlots.Length;
	}
}
