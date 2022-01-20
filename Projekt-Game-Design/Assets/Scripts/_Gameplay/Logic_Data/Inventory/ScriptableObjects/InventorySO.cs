using System.Collections.Generic;
using GDP01.Equipment;
using UnityEngine;

/// <summary>
/// InventorySO
/// All items a player has (all equipped and all not equipped items in possession)
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject {
	
		public List<ItemSO> playerInventory;

		
}
