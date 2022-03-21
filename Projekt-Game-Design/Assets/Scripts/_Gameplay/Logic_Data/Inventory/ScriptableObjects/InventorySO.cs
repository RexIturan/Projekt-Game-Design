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
		public List<ItemSO.ItemTypeData> inventory;
	}
	
	public List<ItemSO> playerInventory;
	
///// Save State ///////////////////////////////////////////////////////////////////////////////////
	
	public InventoryData Save() {
		return new InventoryData {
			inventory = playerInventory.Select(item => item?.ToData()).ToList(),
		};
	}

	public void Load(InventoryData data) {
		playerInventory.Clear();
		if ( data.inventory != null ) {
			playerInventory = new List<ItemSO>(data.inventory.Select(itemData => itemData?.obj));
		}
	}
}
