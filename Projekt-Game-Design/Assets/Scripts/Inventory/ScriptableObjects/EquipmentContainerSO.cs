using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Equipment Inventory Container
/// Container for all EquipmentInventories 
/// </summary>
[CreateAssetMenu(fileName = "New Equipment Container", menuName = "Inventory/EquipmentContainer")]
public class EquipmentContainerSO : ScriptableObject
{
		[SerializeField] private InventorySO inventory;
    public List<Equipment> equipmentInventories;

		public List<ItemSO> GetEquippedItems(int equipmentID) {
				List<ItemSO> items = new List<ItemSO>();
				if ( equipmentID >= equipmentInventories.Count || equipmentID < 0 )
				{
						return null;
						//Debug.LogWarning("ID, " + equipmentID + ", was no valid Equipment (Inventory)");
				}
				else
				{
						foreach ( int inventoryID in equipmentInventories[equipmentID].items )
						{
								items.Add(inventory.GetItem(inventoryID));
						}
				}
				return items;
		}
}
