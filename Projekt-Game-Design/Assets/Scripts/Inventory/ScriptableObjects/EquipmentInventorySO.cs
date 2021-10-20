using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Equipment Inventory SO
/// Has a List of Item, with certain constraints
/// An Equipment Inventory represents all items that are equipped by a Character 
/// </summary>
[CreateAssetMenu(fileName = "New EquipmentInventory", menuName = "Inventory/EquipmentInventory")]
public class EquipmentInventorySO : ScriptableObject {
	private const int MaxEquipmentInventorySize = 7;
	// todo add item constraints
    public List<ItemSO> inventory = new List<ItemSO>(MaxEquipmentInventorySize);
}
